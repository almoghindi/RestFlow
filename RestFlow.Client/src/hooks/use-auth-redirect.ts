"use client";

import { useSelector } from "react-redux";
import { useRouter, usePathname } from "next/navigation";
import { useLayoutEffect } from "react";
import { RootState } from "../store/store";

const useAuthRedirect = () => {
  const router = useRouter();
  const pathname = usePathname();

  const restaurant = useSelector((state: RootState) => state.restaurant);
  const waiter = useSelector((state: RootState) => state.waiter);
  const manager = useSelector((state: RootState) => state.manager);

  useLayoutEffect(() => {
    const isRestaurantRoute = pathname.startsWith("/restaurant");
    const isWaiterRoute = pathname.startsWith("/restaurant/waiter");
    const isManagerRoute = pathname.startsWith("/restaurant/manager");

    if (!restaurant.id) {
      router.push("/");
    } else {
      if (!waiter.id && !manager.id && !isRestaurantRoute) {
        router.push("/restaurant");
      } else if (waiter.id && !manager.id && !isWaiterRoute) {
        router.push("/restaurant/waiter");
      } else if (manager.id && !waiter.id && !isManagerRoute) {
        router.push("/restaurant/manager");
      }
    }
  }, [restaurant, waiter, manager, router, pathname]);
};

export default useAuthRedirect;
