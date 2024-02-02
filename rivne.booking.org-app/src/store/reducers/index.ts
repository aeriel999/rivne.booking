import { combineReducers } from "redux";
import UserReducer from "./userReducers";
import ApartmentReducer from "./apartmentReducers"

export const rootReducer = combineReducers({
  UserReducer,
  ApartmentReducer,
});

export type RootState = ReturnType<typeof rootReducer>;