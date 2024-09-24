"use client";
import { useEffect, useState } from "react";
import useData from "./handle-data";
import { sectionIdFields, SectionNames, sectionToLabel } from "./sections";

const useManagerData = (restaurantId: string) => {
  const { fetchData, saveData, deleteData } = useData();
  const [data, setData] = useState<{ [key: string]: any[] }>({
    Categories: [],
    Ingredients: [],
    Waiters: [],
    Tables: [],
    Dishes: [],
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentSection, setCurrentSection] = useState<string>("Categories");
  const [currentItem, setCurrentItem] = useState<any | null>(null);
  const [editMode, setEditMode] = useState(false);

  const loadData = async () => {
    try {
      setLoading(true);
      const categories = await fetchData("Category", { restaurantId });
      const ingredients = await fetchData("Ingredient", { restaurantId });
      const waiters = await fetchData("Waiter", { restaurantId });
      const tables = await fetchData("Table", { restaurantId });
      const dishes = await fetchData("Dish", { restaurantId });

      setData({
        Categories: categories.$values || categories || [],
        Ingredients: ingredients.$values || ingredients || [],
        Waiters: waiters.$values || waiters || [],
        Tables: tables.$values || tables || [],
        Dishes: dishes.$values || dishes || [],
      });
    } catch (err) {
      setError("Failed to load data.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
  }, [restaurantId]);

  const toggleModal = () => setIsModalOpen(!isModalOpen);

  const handleSave = async () => {
    if (currentSection && currentItem) {
      try {
        const sectionNameSingular =
          sectionToLabel[currentSection as SectionNames];
        const idField = sectionIdFields[currentSection];
        let requestItem = { ...currentItem };

        if (!currentItem.restaurantId) {
          requestItem = { ...currentItem, restaurantId };
        }
        if (currentSection === "Dishes") {
          const selectedIngredientsAsNumbers = requestItem.ingredientIds?.map(
            (id: string) => parseInt(id)
          );
          requestItem = {
            ...requestItem,
            ingredientIds: selectedIngredientsAsNumbers,
          };
        }

        const responseData = await saveData(
          sectionNameSingular,
          requestItem,
          editMode ? currentItem[idField] : undefined
        );

        setData((prevData) => ({
          ...prevData,
          [currentSection]: editMode
            ? prevData[currentSection].map((i) =>
                i[idField] === responseData[idField] ? responseData : i
              )
            : [...prevData[currentSection], responseData],
        }));

        toggleModal();
        loadData();
      } catch (error) {
        console.error(`Failed to save ${currentSection}:`, error);
      }
    }
  };

  const handleDeleteItem = async (item: any) => {
    try {
      const sectionNameSingular =
        sectionToLabel[currentSection as SectionNames];
      const idField = sectionIdFields[currentSection];

      await deleteData(sectionNameSingular, item[idField]);
      loadData();
    } catch (error) {
      console.error(`Failed to delete item:`, error);
    }
  };

  return {
    data,
    loading,
    error,
    isModalOpen,
    currentSection,
    currentItem,
    editMode,
    toggleModal,
    setCurrentSection,
    setCurrentItem,
    setEditMode,
    handleSave,
    handleDeleteItem,
  };
};

export default useManagerData;
