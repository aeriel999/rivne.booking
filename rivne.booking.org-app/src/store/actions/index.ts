import * as userActionCreator from "./userActionCreator";
import  * as apartmentActionCreator from "./apartmentActionCreator";

export default {
  ...userActionCreator,
  ...apartmentActionCreator,
};
