"use client";

import { useState, useEffect } from "react";

export const useTabLogic = (dishes: any[], categories: any[]) => {
  const [activeTab, setActiveTab] = useState<string | null>(null);
  const [filteredDishes, setFilteredDishes] = useState<any[]>([]);

  useEffect(() => {
    if (categories.length > 0) {
      setActiveTab(categories[0].categoryId);
    }
  }, [categories]);

  useEffect(() => {
    if (activeTab && dishes.length > 0) {
      const filtered = dishes.filter((dish) => dish.categoryId === activeTab);
      setFilteredDishes(filtered);
    }
  }, [activeTab, dishes]);

  const handleTabClick = (categoryId: string) => {
    setActiveTab(categoryId);
  };

  return { activeTab, filteredDishes, handleTabClick };
};
