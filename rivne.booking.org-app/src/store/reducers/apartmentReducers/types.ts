export interface ApartmentState {
  apartment: any,
  message: null,
  loading: false,
  error: null,
  selectedApartment: any,
  allApartment: [],
  streetsList: []
};

export enum ApartmentActionTypes {
  GET_ALL_APARTMENTS_SUCCESS = 'GET_ALL_APARTMENTS_SUCCESS',
  GET_ALL_APARTMENTS_ERROR = 'GET_ALL_USERS_ERROR',
  SERVER_ERROR = 'SERVER_ERROR ',
  DELETE_APARTMENT_SUCCESS = 'DELETE_APARTMENT_SUCCESS',
  DELETE_APARTMENT_ERROR = 'DELETE_APARTMENT_ERROR',
  ADD_APARTMENT_SUCCESS = 'ADD_APARTMENT_SUCCESS',
  ADD_APARTMENT_ERROR = 'ADD_APARTMENT_ERROR',
  GET_STREETS_LIST_SUCCESS = 'GET_STREETS_LIST_SUCCESS',
  GET_STREETS_LIST_ERROR = 'GET_STREETS_LIST_ERROR',
  GET_APARTMENT_SUCCESS = 'GET_APARTMENT_SUCCESS',
  GET_APARTMENT_ERROR = 'GET_APARTMENT_ERROR',
  EDIT_APARTMENT_SUCCESS = 'GET_APARTMENT_SUCCESS',
  EDIT_APARTMENT_ERROR = 'GET_APARTMENT_ERROR',
}

interface GetAllApartmentsSuccessAction {
  type: ApartmentActionTypes.GET_ALL_APARTMENTS_SUCCESS;
  payload: any;
}

interface GetAllApartmentsErrorAction {
  type: ApartmentActionTypes.GET_ALL_APARTMENTS_ERROR;
  payload: any;
}

interface ServerErrorAction {
  type: ApartmentActionTypes.SERVER_ERROR;
  payload: any;
}

interface DeleteApartmentSuccessAction {
  type: ApartmentActionTypes.DELETE_APARTMENT_SUCCESS;
  payload: any;
}

interface DeleteApartmentErrorAction {
  type: ApartmentActionTypes.DELETE_APARTMENT_ERROR;
  payload: any;
}

interface AddApartmentSuccessAction {
  type: ApartmentActionTypes.ADD_APARTMENT_SUCCESS;
  payload: any;
}

interface AddApartmentErrorAction {
  type: ApartmentActionTypes.ADD_APARTMENT_ERROR;
  payload: any;
}

interface GetStreetsListSuccessAction {
  type: ApartmentActionTypes.GET_STREETS_LIST_SUCCESS;
  payload: any;
}

interface GetStreetsListErrorAction {
  type: ApartmentActionTypes.GET_STREETS_LIST_ERROR;
  payload: any;
}

interface GetApartmentSuccessAction {
  type: ApartmentActionTypes.GET_APARTMENT_SUCCESS;
  payload: any;
}

interface GetApartmentErrorAction {
  type: ApartmentActionTypes.GET_APARTMENT_ERROR;
  payload: any;
}

interface EditApartmentSuccessAction {
  type: ApartmentActionTypes.EDIT_APARTMENT_SUCCESS;
  payload: any;
}

interface EditApartmentErrorAction {
  type: ApartmentActionTypes.EDIT_APARTMENT_ERROR;
  payload: any;
}
export type ApartmentActions =
  | GetAllApartmentsSuccessAction
  | GetAllApartmentsErrorAction
  | ServerErrorAction
  | DeleteApartmentErrorAction
  | DeleteApartmentSuccessAction
  | AddApartmentSuccessAction
  | AddApartmentErrorAction
  | GetStreetsListSuccessAction
  | GetStreetsListErrorAction
  | GetApartmentSuccessAction
  | GetApartmentErrorAction
  |EditApartmentSuccessAction
  |EditApartmentErrorAction;
