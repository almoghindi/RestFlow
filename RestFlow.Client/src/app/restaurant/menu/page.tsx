"use client";
import { store } from "@/store/store";
import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import useAuthRedirect from "@/hooks/use-auth-redirect";
import { Button } from "@/components/ui/button";
import TabsSection from "./tabs-section";
import DishesSection from "./dishes-section";
import { useMenuData } from "./use-menu-data";
import { useTabLogic } from "./use-tab-logic";

const MenuPage: React.FC = () => {
  useAuthRedirect();
  const router = useRouter();
  const [restaurant, setRestaurant] = useState<{
    id: string | null;
    name: string;
  }>({ id: null, name: "" });

  useEffect(() => {
    const restaurantData = store.getState().restaurant;
    setRestaurant({
      id: restaurantData?.id ? restaurantData.id.toString() : null,
      name: restaurantData.name,
    });
    if (!restaurantData?.id) {
      router.push("/");
    }
  }, [router]);

  const { categories, dishes } = useMenuData(restaurant.id);
  const { activeTab, filteredDishes, handleTabClick } = useTabLogic(
    dishes,
    categories
  );

  const formatIngredients = (ingredients: { name: string }[]) => {
    const ingredientNames = ingredients.map((ingredient) => ingredient.name);
    return ingredientNames.length > 1
      ? `${ingredientNames.slice(0, -1).join(", ")} and ${ingredientNames[ingredientNames.length - 1]}`
      : ingredientNames[0];
  };

  return (
    <div
      className="bg-black-900 text-white p-4 font-sans flex flex-col w-screen bg-cover bg-center"
      style={{
        backgroundImage: "url('/menu.jpg')",
        height: "calc(100vh - 3rem)",
      }}
    >
      <div className="absolute inset-0 bg-black opacity-80" />
      <div className="relative z-10 flex flex-col h-full">
        <div className="flex justify-between items-center mb-4">
          <h1 className="text-2xl font-bold">{restaurant.name}</h1>
          <div className="flex items-center">
            <span className="mr-2">ENG</span>
          </div>
        </div>
        <div className="flex flex-1 w-full">
          <TabsSection
            categories={categories}
            activeTab={activeTab}
            onTabClick={handleTabClick}
          />
          <div
            className="w-3/4 flex flex-col h-full"
            style={{
              overflowY: "auto",
              scrollbarWidth: "none",
              msOverflowStyle: "none",
            }}
          >
            <style jsx>{`
              ::-webkit-scrollbar {
                display: none;
              }
            `}</style>
            <div
              className="flex-1 pr-4"
              style={{ maxHeight: "calc(100vh - 9rem)", overflowY: "auto" }}
            >
              <DishesSection
                dishes={filteredDishes}
                formatIngredients={formatIngredients}
              />
            </div>
            <div className="text-center mt-4" style={{ paddingBottom: "16px" }}>
              <Button
                variant="secondary"
                onClick={() => router.back()}
                style={{ marginTop: "auto", position: "sticky", bottom: "0" }}
              >
                Back
              </Button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MenuPage;
