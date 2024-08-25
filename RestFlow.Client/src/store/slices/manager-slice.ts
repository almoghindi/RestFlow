import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface ManagerState {
  id: number | null;
  name: string;
}

const initialState: ManagerState = {
  id: null,
  name: "",
};

const managerSlice = createSlice({
  name: "manager",
  initialState,
  reducers: {
    setManager(state, action: PayloadAction<ManagerState>) {
      state.id = action.payload.id;
      state.name = action.payload.name;
    },
    logout(state) {
      state.id = null;
      state.name = "";
    },
  },
});

export const { setManager, logout } = managerSlice.actions;
export default managerSlice.reducer;
