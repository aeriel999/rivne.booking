import { useDispatch } from "react-redux";
import { bindActionCreators } from "redux";
import userActionCreator from "../store/actions";

export const useActions = () => {
  const dispatch = useDispatch();
  return bindActionCreators(userActionCreator, dispatch);
};
