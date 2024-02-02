import { ApartmentActions, ApartmentActionTypes, ApartmentState } from './types.ts';

const initialState: ApartmentState = {
  apartment: {},
  message: null,
  loading: false,
  error: null,
  selectedApartment: {},
  allApartment: [],
  streetsList: []
};

const ApartmentReducer = (state = initialState, action: ApartmentActions): ApartmentState => {

  switch (action.type) {
    case ApartmentActionTypes.GET_ALL_APARTMENTS_SUCCESS:
      return {
        ...state,
        loading: false,
        allApartment: action.payload,
      };
    case ApartmentActionTypes.GET_ALL_APARTMENTS_ERROR:
      return {
        ...state,
        message: action.payload.message
      };
    case ApartmentActionTypes.DELETE_APARTMENT_SUCCESS:
      return {
        ...state,
        loading: false,
        message: action.payload,
      };
    case ApartmentActionTypes.DELETE_APARTMENT_ERROR:
      return {
        ...state,
        loading: false,
        message: action.payload.message,
      };
    case ApartmentActionTypes.GET_STREETS_LIST_SUCCESS:
      return {
        ...state,
        loading: false,
        streetsList: action.payload,
      };
    case ApartmentActionTypes.GET_STREETS_LIST_ERROR:
      return {
        ...state,
        loading: false,
        message: action.payload.message,
      };
    case ApartmentActionTypes.GET_APARTMENT_SUCCESS:
      return {
        ...state,
        loading: false,
        selectedApartment: action.payload
      }
    case ApartmentActionTypes.GET_APARTMENT_ERROR:
      return {
        ...state,
        message: action.payload.message
      };
    case ApartmentActionTypes.EDIT_APARTMENT_SUCCESS:
    return {
      ...state,
      message: action.payload.message
    };
    case ApartmentActionTypes.EDIT_APARTMENT_ERROR:
      return {
        ...state,
        message: action.payload.message
      };
    case ApartmentActionTypes.SERVER_ERROR:
      return { ...state, loading: false };
    default: return  state;
  }
}

export  default  ApartmentReducer;