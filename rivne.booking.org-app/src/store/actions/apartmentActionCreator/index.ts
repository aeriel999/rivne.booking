import { ApartmentActions, ApartmentActionTypes } from '../../reducers/apartmentReducers/types.ts';
import { Dispatch } from 'redux';
import {
  addApartment,
  deleteApartment, editApartment,
  getAll,
  getApartment,
  getStreetsList
} from '../../../services/apartmentServices';


export const GetAllApartments = () => {
  return async (dispatch: Dispatch<ApartmentActions>) => {
    try {
      const data = await getAll();
      console.log("data.data.payLoad", data.data.payLoad);
      if (data.data.success) {
        dispatch({
          type: ApartmentActionTypes.GET_ALL_APARTMENTS_SUCCESS,
          payload: data.data.payLoad ,
        });
      } else {
        dispatch({
          type: ApartmentActionTypes.GET_ALL_APARTMENTS_ERROR,
          payload: data.data.message,
        });
      }
    } catch (error) {
      dispatch({
        type: ApartmentActionTypes.SERVER_ERROR,
        payload: "Unknown error",
      });
      // toast.error("Unknown get error");
    }
  };
};

export const DeleteApartment = (userId: number) => {
  return async (dispatch: Dispatch<ApartmentActions>) => {
    try {
      const data = await deleteApartment(userId);

      if (!data.data.success) {
        dispatch({
          type: ApartmentActionTypes.DELETE_APARTMENT_ERROR,
          payload: data.data.message,
        });
        //toast.error(data.data.message);
      } else {
        dispatch({
          type: ApartmentActionTypes.DELETE_APARTMENT_SUCCESS,
          payload: data.data.payLoad,
        });

        // toast.success(data.data.message);
      }
    } catch (e) {
      dispatch({
        type: ApartmentActionTypes.SERVER_ERROR,
        payload: "Unknown error",
      });
      //toast.error("Unknown delete error");
    }
  };
};

export const AddApartment = (model: any) => {
  console.log("AddApartment", model);
  return async (dispatch: Dispatch<ApartmentActions>) => {
    try {
      const data = await addApartment(model);

      if (!data.data.success) {
        dispatch({
          type: ApartmentActionTypes.ADD_APARTMENT_ERROR,
          payload: data.data.message,
        });
        //toast.error(data.data.message);
      } else {
        dispatch({
          type: ApartmentActionTypes.ADD_APARTMENT_SUCCESS,
          payload: data.data.payLoad,
        });

        // toast.success(data.data.message);
      }
    } catch (e) {
      dispatch({
        type: ApartmentActionTypes.SERVER_ERROR,
        payload: "Unknown error",
      });
      //toast.error("Unknown delete error");
    }
  };
};

export const GetStreetsList = () => {
  return async (dispatch: Dispatch<ApartmentActions>) => {
    try {
      const data = await getStreetsList();
      if (data.data.success) {
        dispatch({
          type: ApartmentActionTypes.GET_STREETS_LIST_SUCCESS,
          payload: data.data.payLoad ,
        });
      } else {
        dispatch({
          type: ApartmentActionTypes.GET_STREETS_LIST_ERROR,
          payload: data.data.message,
        });
      }
    } catch (error) {
      dispatch({
        type: ApartmentActionTypes.SERVER_ERROR,
        payload: "Unknown error",
      });
      // toast.error("Unknown get error");
    }
  };
};

export const GetApartment= (apartmentId : number) => {
  console.log("GetApartment", apartmentId)
  return async (dispatch: Dispatch<ApartmentActions>) => {
    try {
      const data = await getApartment(apartmentId);
      if (data.data.success) {
        dispatch({
          type: ApartmentActionTypes.GET_APARTMENT_SUCCESS,
          payload: data.data.payLoad ,
        });
      } else {
        dispatch({
          type: ApartmentActionTypes.GET_APARTMENT_ERROR,
          payload: data.data.message,
        });
      }
    } catch (error) {
      dispatch({
        type: ApartmentActionTypes.SERVER_ERROR,
        payload: "Unknown error",
      });
      // toast.error("Unknown get error");
    }
  };
};

export const EditApartment = (model: any) => {
  console.log("AddApartment", model);
  return async (dispatch: Dispatch<ApartmentActions>) => {
    try {
      const data = await editApartment(model);

      if (!data.data.success) {
        dispatch({
          type: ApartmentActionTypes.EDIT_APARTMENT_ERROR,
          payload: data.data.message,
        });
        //toast.error(data.data.message);
      } else {
        dispatch({
          type: ApartmentActionTypes.EDIT_APARTMENT_SUCCESS,
          payload: data.data.payLoad,
        });

        // toast.success(data.data.message);
      }
    } catch (e) {
      dispatch({
        type: ApartmentActionTypes.SERVER_ERROR,
        payload: "Unknown error",
      });
      //toast.error("Unknown delete error");
    }
  };
};
