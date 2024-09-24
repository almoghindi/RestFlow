"use client";
import { useState, useEffect } from "react";
import useRequest from "@/hooks/use-request";

export const useMenuData = (restaurantId: string | null) => {
  const { sendRequest } = useRequest();
  const [categories, setCategories] = useState<any[]>([]);
  const [dishes, setDishes] = useState<any[]>([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await sendRequest({
          url: `/api/category?restaurantId=${restaurantId}`,
          method: "GET",
        });
        setCategories(response);
      } catch (error) {
        console.error(error);
        throw error;
      }
    };

    const fetchDishes = async () => {
      try {
        const response = await sendRequest({
          url: `/api/dish?restaurantId=${restaurantId}`,
          method: "GET",
        });
        setDishes(response);
      } catch (error) {
        console.error(error);
        throw error;
      }
    };

    if (restaurantId) {
      fetchCategories();
      fetchDishes();
    }
  }, [restaurantId]);

  return { categories, dishes };
};
