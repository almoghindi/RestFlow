"use client";
import { Button } from "@/components/ui/button";
import {
  Restaurant03Icon,
  WaiterIcon,
  ManagerIcon,
  ChefIcon,
  Logout03Icon,
} from "hugeicons-react";
import { store } from "../../store/store";
import useAuthRedirect from "@/hooks/use-auth-redirect";
import Typography from "@/components/ui/typography";
import { useEffect } from "react";
import React from "react";
import { removeFromLocalStorage } from "@/lib/local-storage";
import { useDispatch } from "react-redux";
import { logout as logoutAction } from "@/store/slices/restaurant-slice";
import { useRouter } from "next/navigation";

const RestaurantPage = () => {
  useAuthRedirect();
  const [restaurantName, setRestaurantName] = React.useState("");
  const dispatch = useDispatch();
  const router = useRouter();

  useEffect(() => {
    setRestaurantName(store.getState().restaurant?.name);
    if (!store.getState().restaurant?.id) {
      router.push("/");
    }
  }, []);

  const logout = () => {
    removeFromLocalStorage("restaurant");
    dispatch(logoutAction());

    router.push("/");
  };

  return (
    <div
      className="flex flex-col items-center justify-center w-screen bg-cover bg-center relative"
      style={{
        backgroundImage: "url('/restaurant.webp')",
        height: "calc(100vh - 3rem)",
      }}
    >
      <div className="absolute inset-0 bg-black opacity-80" />

      <div className="relative z-10 flex flex-col items-center w-full max-w-lg lg:max-w-3xl xl:max-w-4xl px-4 lg:px-8">
        <Typography variant="h1">Hey, {restaurantName}</Typography>

        <div className="grid grid-cols-2 sm:grid-cols-2 gap-8 mt-12 w-full max-w-lg">
          <Button
            variant="ghost"
            className="p-8"
            onClick={() => router.push("/restaurant/menu")}
          >
            <Restaurant03Icon className="text-5xl m-1" />
            <span className="text-xl">Menu</span>
          </Button>
          <Button variant="ghost" className="p-8">
            <WaiterIcon className="text-5xl m-1" />
            <span className="text-xl">Waiter</span>
          </Button>
          <Button
            variant="ghost"
            className="p-8"
            onClick={() => router.push("/restaurant/manager-login")}
          >
            <ManagerIcon className="text-5xl m-1" />
            <span className="text-xl">Manager</span>
          </Button>
          <Button variant="ghost" className="p-8">
            <ChefIcon className="text-5xl m-1" />
            <span className="text-xl">Cooker</span>
          </Button>
        </div>

        <Button
          onClick={logout}
          className="w-full max-w-lg py-5 bg-red-500 text-white font-semibold rounded-lg hover:bg-red-600 mt-24"
        >
          <Logout03Icon className="text-3xl mr-2" />
          Logout
        </Button>
      </div>
    </div>
  );
};

export default RestaurantPage;
