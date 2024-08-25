import { configureStore } from "@reduxjs/toolkit";
import restaurantReducer from "./slices/restaurant-slice";
import waiterReducer from "./slices/waiter-slice";
import managerReducer from "./slices/manager-slice";
import {
  loadFromLocalStorage,
  saveToLocalStorage,
} from "../hooks/use-local-storage";

const preloadedState = {
  restaurant: loadFromLocalStorage("restaurant") || { id: null, name: "" },
  waiter: loadFromLocalStorage("waiter") || { id: null, name: "" },
  manager: loadFromLocalStorage("manager") || { id: null, name: "" },
};

const store = configureStore({
  reducer: {
    restaurant: restaurantReducer,
    waiter: waiterReducer,
    manager: managerReducer,
  },
  preloadedState,
});

store.subscribe(() => {
  const state = store.getState();
  saveToLocalStorage("restaurant", state.restaurant);
  saveToLocalStorage("waiter", state.waiter);
  saveToLocalStorage("manager", state.manager);
});

export default store;
