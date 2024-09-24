import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface RestaurantState {
  id: number | null;
  name: string;
}

const initialState: RestaurantState = {
  id: null,
  name: "",
};

const restaurantSlice = createSlice({
  name: "restaurant",
  initialState,
  reducers: {
    setRestaurant(state, action: PayloadAction<RestaurantState>) {
      state.id = action.payload.id;
      state.name = action.payload.name;
    },
    logout(state) {
      state.id = null;
      state.name = "";
    },
  },
});

export const { setRestaurant, logout } = restaurantSlice.actions;
export default restaurantSlice.reducer;
