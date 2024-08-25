import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface WaiterState {
  id: number | null;
  name: string;
}

const initialState: WaiterState = {
  id: null,
  name: "",
};

const waiterSlice = createSlice({
  name: "waiter",
  initialState,
  reducers: {
    setWaiter(state, action: PayloadAction<WaiterState>) {
      state.id = action.payload.id;
      state.name = action.payload.name;
    },
    logout(state) {
      state.id = null;
      state.name = "";
    },
  },
});

export const { setWaiter, logout } = waiterSlice.actions;
export default waiterSlice.reducer;
