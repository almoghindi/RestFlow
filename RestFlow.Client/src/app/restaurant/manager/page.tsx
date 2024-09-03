"use client";
import React, { useState, useEffect } from "react";
import { Modal } from "@/components/ui/modal";
import DashboardSection from "./dashboard-section";
import { sectionsConfig } from "./sections";
import useData from "./handle-data";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@/store/store";
import { LoadingSpinner } from "@/components/ui/loading-spinner";
import { Button } from "@/components/ui/button";
import useAuthRedirect from "@/hooks/use-auth-redirect";
import { Logout03Icon } from "hugeicons-react";
import { removeFromLocalStorage } from "@/lib/local-storage";
import { useRouter } from "next/navigation";
import { logout as logoutAction } from "@/store/slices/manager-slice";
import Typography from "@/components/ui/typography";
import { Checkbox } from "@/components/ui/checkbox";
import { Dropdown } from "@/components/ui/dropdown";

const ManagerPage: React.FC = () => {
  useAuthRedirect();

  const dispatch = useDispatch();
  const router = useRouter();
  const { fetchData, saveData, deleteData } = useData();
  const restaurant = useSelector((state: RootState) => state.restaurant);

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
  const [currentSection, setCurrentSection] = useState<string>("Tables");
  const [currentItem, setCurrentItem] = useState<any | null>(null);
  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    const loadData = async () => {
      try {
        setLoading(true);
        const categories = await fetchData("Category", {
          restaurantId: restaurant.id,
        });
        const ingredients = await fetchData("Ingredient", {
          restaurantId: restaurant.id,
        });
        const waiters = await fetchData("Waiter", {
          restaurantId: restaurant.id,
        });
        const tables = await fetchData("Table", {
          restaurantId: restaurant.id,
        });
        const dishes = await fetchData("Dish", { restaurantId: restaurant.id });

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

    loadData();
  }, [restaurant.id]);

  const toggleModal = () => setIsModalOpen(!isModalOpen);

  const handleSave = async () => {
    if (currentSection && currentItem) {
      try {
        const id = currentItem.id;
        const responseData = await saveData(currentSection, currentItem, id);
        setData((prevData) => ({
          ...prevData,
          [currentSection]: editMode
            ? prevData[currentSection].map((i) =>
                i.id === responseData.id ? responseData : i
              )
            : [...prevData[currentSection], responseData],
        }));
        toggleModal();
      } catch (error) {
        console.error(`Failed to save ${currentSection}:`, error);
      }
    }
  };

  const handleDeleteItem = async (section: string, item: any) => {
    try {
      await deleteData(section, item.id);
      setData((prevData) => ({
        ...prevData,
        [section]: prevData[section].filter((i) => i.id !== item.id),
      }));
    } catch (error) {
      console.error(`Failed to delete ${section}:`, error);
    }
  };

  const handleTabClick = (section: string) => {
    setCurrentSection(section);
    setCurrentItem(null);
    setEditMode(false);
  };

  const logout = () => {
    removeFromLocalStorage("manager");
    dispatch(logoutAction());

    router.push("/restaurant");
  };

  return (
    <div
      className="flex flex-col items-center w-screen bg-cover bg-center relative"
      style={{
        backgroundImage: "url('/management.jpg')",
        height: "calc(100vh - 3rem)",
      }}
    >
      <div className="absolute inset-0 bg-black opacity-80" />
      <div className="relative z-10">
        <h1 className="text-4xl font-bold mb-6 mt-28">Restaurant Management</h1>
        <div className="mb-6">
          {sectionsConfig.map((section) => (
            <Button
              key={section.name}
              onClick={() => handleTabClick(section.name)}
              variant={currentSection === section.name ? "ghost" : "outline"}
              className="mr-4 mb-2 text-lg py-2 px-6"
            >
              {section.name}
            </Button>
          ))}
        </div>
        <div className="mb-6">
          <h2 className="text-xl font-bold mb-4">{currentSection}</h2>
          {loading ? (
            <LoadingSpinner />
          ) : error ? (
            <p className="text-red-500">Error fetching data.</p>
          ) : (
            <DashboardSection
              title={currentSection}
              items={data[currentSection] || []}
              fields={
                sectionsConfig.find((config) => config.name === currentSection)
                  ?.fields || []
              }
              onAdd={() => {
                setEditMode(false);
                setCurrentItem(null);
                toggleModal();
              }}
              onEdit={(item) => {
                setCurrentItem(item);
                setEditMode(true);
                toggleModal();
              }}
              onDelete={(item) => handleDeleteItem(currentSection, item)}
              categories={data.Categories}
            />
          )}
          <div className="mb-6 flex justify-end">
            <Button
              onClick={logout}
              className="w-max py-5 bg-red-500 text-white font-semibold rounded-lg hover:bg-red-600 mt-24"
            >
              <Logout03Icon className="text-3xl mr-2" />
              Logout
            </Button>
          </div>
        </div>
        <Modal
          title={editMode ? `Edit ${currentSection}` : `Add ${currentSection}`}
          isOpen={isModalOpen}
          onClose={toggleModal}
          onSave={handleSave}
          saveButtonText={editMode ? "Update" : "Save"}
        >
          {currentItem ? (
            <div className="space-y-4">
              <Typography className="font-medium text-black" variant="h3">
                {editMode ? `Edit ${currentSection}` : `Add ${currentSection}`}
              </Typography>
              {sectionsConfig
                .find((config) => config.name === currentSection)
                ?.fields.map((field) => (
                  <div key={field.name} className="flex items-center">
                    <label className="w-1/3 text-sm font-medium text-gray-700">
                      {field.label}
                    </label>
                    {field.name === "CategoryId" ? (
                      <Dropdown
                        items={data.Categories.map((cat) => ({
                          value: cat.categoryId,
                          name: cat.name,
                        }))}
                        value={currentItem?.categoryId || ""}
                        onChange={(value) => {
                          const selectedCategory = data.Categories.find(
                            (cat) => cat.id === value
                          );
                          setCurrentItem({
                            ...currentItem,
                            categoryId: value,
                            category: selectedCategory
                              ? selectedCategory.name
                              : "",
                          });
                        }}
                      />
                    ) : field.name === "isAvailable" ? (
                      <Checkbox
                        checked={!!currentItem[field.name]}
                        onCheckedChange={(checked) =>
                          setCurrentItem({
                            ...currentItem,
                            [field.name]: checked,
                          })
                        }
                      />
                    ) : (
                      <input
                        type="text"
                        className="w-2/3 mt-1 block px-3 py-2 border text-black border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                        value={currentItem[field.name] || ""}
                        onChange={(e) =>
                          setCurrentItem({
                            ...currentItem,
                            [field.name]: e.target.value,
                          })
                        }
                      />
                    )}
                  </div>
                ))}
            </div>
          ) : (
            <p>No item selected for editing.</p>
          )}
        </Modal>
      </div>{" "}
    </div>
  );
};

export default ManagerPage;
