import { configureStore } from "@reduxjs/toolkit";
import restaurantReducer from "./slices/restaurant-slice";
import waiterReducer from "./slices/waiter-slice";
import managerReducer from "./slices/manager-slice";
import { saveToLocalStorage, loadFromLocalStorage } from "../lib/local-storage";
import { decryptData, encryptData } from "../lib/encryption";

interface PreloadedState {
  restaurant: ReturnType<typeof restaurantReducer>;
  waiter: ReturnType<typeof waiterReducer>;
  manager: ReturnType<typeof managerReducer>;
}

const safeParse = (data: string): any => {
  try {
    return JSON.parse(data);
  } catch (error) {
    return { id: null, name: "" };
  }
};

const preloadedState: PreloadedState = {
  restaurant: loadFromLocalStorage("restaurant")
    ? safeParse(decryptData(loadFromLocalStorage("restaurant") || ""))
    : { id: null, name: "" },
  waiter: loadFromLocalStorage("waiter")
    ? safeParse(decryptData(loadFromLocalStorage("waiter") || ""))
    : { id: null, name: "" },
  manager: loadFromLocalStorage("manager")
    ? safeParse(decryptData(loadFromLocalStorage("manager") || ""))
    : { id: null, name: "" },
};

export const store = configureStore({
  reducer: {
    restaurant: restaurantReducer,
    waiter: waiterReducer,
    manager: managerReducer,
  },
  preloadedState,
});

store.subscribe(() => {
  const state = store.getState();

  if (state.restaurant.id !== null && state.restaurant.name !== "") {
    saveToLocalStorage(
      "restaurant",
      encryptData(JSON.stringify(state.restaurant))
    );
  }
  if (state.waiter.id !== null && state.waiter.name !== "") {
    saveToLocalStorage("waiter", encryptData(JSON.stringify(state.waiter)));
  }
  if (state.manager.id !== null && state.manager.name !== "") {
    saveToLocalStorage("manager", encryptData(JSON.stringify(state.manager)));
  }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
