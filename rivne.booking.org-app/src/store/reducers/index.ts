import { combineReducers } from "redux";
import UserReducer from "./userReducers";

export const rootReducer = combineReducers({
  UserReducer,
});

export type RootState = ReturnType<typeof rootReducer>;