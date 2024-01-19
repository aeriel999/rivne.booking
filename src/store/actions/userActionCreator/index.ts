import { UserActionTypes, UserActions } from "../../reducers/userReducers/types.ts";
//import services
import {
    login,
    // logout,
    // getAll,
    // addUser,
    setAccessToken,
    setRefreshToken,
   // removeTokens,
    // getUser,
    // editUser,
    // deleteUser,
    // changePassword,
} from "../../../services/userServices";
import { Dispatch } from "redux";
import { toast } from "react-toastify";
import jwtDecode from "jwt-decode";
import { ILogin } from '../../../interfaces/user';

export const LoginUser = (user: ILogin) => {
    return async (dispatch: Dispatch<UserActions>) => {
        try {
            dispatch({ type: UserActionTypes.START_REQUEST });

            const data = await login(user);

            //const { response } = data.data;
            console.log("data.data", data.data);

            if (!data.data.success) {

                dispatch({
                    type: UserActionTypes.LOGIN_USER_ERROR,
                    payload: data.data.message,
                });
                toast.error(data.data.message);
            } else {

               // toast.success(data.data.message);
                const { accessToken, refreshToken, message } = data.data;

                setAccessToken(accessToken);
                setRefreshToken(refreshToken);
                AuthUther(accessToken, message, dispatch);
            }
        } catch (e) {
            dispatch({
                type: UserActionTypes.SERVER_ERROR,
                payload: "Unknown error",
            });
         //   toast.error("Unknown login error");
        }
    };
};

export const AuthUther = (
    token: string,
    message: string,
    dispatch: Dispatch<UserActions>
) => {
    const decodedToken = jwtDecode(token) as any;
    dispatch({
        type: UserActionTypes.LOGIN_USER_SUCCESS,
        payload: { message, decodedToken },
    });
};

// export const LogOut = (id: string) => {
//     return async (dispatch: Dispatch<UserActions>) => {
//         const data = await logout(id);
//         const { response } = data;
//
//         if (response.success) {
//             removeTokens();
//             dispatch({
//                 type: UserActionTypes.LOGOUT_USER_SUCCESS,
//                 payload: response.message,
//             });
//         } else {
//             dispatch({
//                 type: UserActionTypes.LOGOUT_USER_ERROR,
//                 payload: response.message,
//             });
//         }
//     };
// };
//
// export const GetAll = () => {
//     return async (dispatch: Dispatch<UserActions>) => {
//         try {
//             dispatch({ type: UserActionTypes.START_REQUEST });
//
//             const data = await getAll();
//
//             const { response } = data;
//
//             if (response.success) {
//                 dispatch({
//                     type: UserActionTypes.GET_ALL_USERS_SUCCESS,
//                     payload: { allUsers: response.payLoad },
//                 });
//             } else {
//                 dispatch({
//                     type: UserActionTypes.GET_ALL_USERS_ERROR,
//                     payload: response.message,
//                 });
//             }
//         } catch (error) {
//             dispatch({
//                 type: UserActionTypes.SERVER_ERROR,
//                 payload: "Unknown error",
//             });
//             toast.error("Unknown get error");
//         }
//     };
// };
//
// export const AddUser = (user: any) => {
//     return async (dispatch: Dispatch<UserActions>) => {
//         try {
//             dispatch({ type: UserActionTypes.START_REQUEST });
//
//             const data = await addUser(user);
//
//             const { response } = data;
//
//             if (!response.success) {
//                 dispatch({
//                     type: UserActionTypes.ADD_USER_ERROR,
//                     payload: response.message,
//                 });
//                 toast.error(response.message);
//             } else {
//                 dispatch({
//                     type: UserActionTypes.ADD_USER_SUCCESS,
//                     payload: response.message,
//                 });
//
//                 toast.success(response.message);
//             }
//         } catch (e) {
//             dispatch({
//                 type: UserActionTypes.SERVER_ERROR,
//                 payload: "Unknown error",
//             });
//             toast.error("Unknown add error");
//         }
//     };
// };
//
// export const GetUser = (userId: string) => {
//     return async (dispatch: Dispatch<UserActions>) => {
//         try {
//             dispatch({ type: UserActionTypes.START_REQUEST });
//
//             const data = await getUser(userId);
//
//             const { response } = data;
//
//             if (!response.success) {
//                 dispatch({
//                     type: UserActionTypes.GET_USER_ERROR,
//                     payload: response.message,
//                 });
//                 toast.error(response.message);
//             } else {
//                 dispatch({
//                     type: UserActionTypes.GET_USER_SUCCESS,
//                     payload: response.payLoad,
//                 });
//
//                 toast.success(response.message);
//             }
//         } catch (e) {
//             dispatch({
//                 type: UserActionTypes.SERVER_ERROR,
//                 payload: "Unknown error",
//             });
//             toast.error("Unknown user error");
//         }
//     };
// };
//
// export const EditUser = (user: any) => {
//     return async (dispatch: Dispatch<UserActions>) => {
//         try {
//             dispatch({ type: UserActionTypes.START_REQUEST });
//
//             const data = await editUser(user);
//
//             const { response } = data;
//
//             if (!response.success) {
//                 dispatch({
//                     type: UserActionTypes.EDIT_USER_ERROR,
//                     payload: response.message,
//                 });
//                 toast.error(response.message);
//             } else {
//                 dispatch({
//                     type: UserActionTypes.EDIT_USER_SUCCESS,
//                     payload: response.message,
//                 });
//                 toast.success(response.message);
//             }
//         } catch (e) {
//             dispatch({
//                 type: UserActionTypes.SERVER_ERROR,
//                 payload: "Unknown error",
//             });
//             toast.error("Unknown edit error");
//         }
//     };
// };
//
// export const DeleteUser = (userId: string) => {
//     return async (dispatch: Dispatch<UserActions>) => {
//         try {
//             dispatch({ type: UserActionTypes.START_REQUEST });
//
//             const data = await deleteUser(userId);
//
//             const { response } = data;
//
//             if (!response.success) {
//                 dispatch({
//                     type: UserActionTypes.DELETE_USER_ERROR,
//                     payload: response.message,
//                 });
//                 toast.error(response.message);
//             } else {
//                 dispatch({
//                     type: UserActionTypes.DELETE_USER_SUCCESS,
//                     payload: response.payLoad,
//                 });
//
//                 toast.success(response.message);
//             }
//         } catch (e) {
//             dispatch({
//                 type: UserActionTypes.SERVER_ERROR,
//                 payload: "Unknown error",
//             });
//             toast.error("Unknown delete error");
//         }
//     };
// };
//
// export const ChangePassword = (passModel: any) => {
//     return async (dispatch: Dispatch<UserActions>) => {
//         try {
//             dispatch({ type: UserActionTypes.START_REQUEST });
//
//             const data = await changePassword(passModel);
//
//             const { response } = data;
//
//             if (!response.success) {
//                 dispatch({
//                     type: UserActionTypes.CHANGE_PASSWORD_ERROR,
//                     payload: response.message,
//                 });
//                 toast.error(response.message);
//             } else {
//                 dispatch({
//                     type: UserActionTypes.CHANGE_PASSWORD_SUCCESS,
//                     payload: response.payLoad,
//                 });
//
//                 toast.success(response.message);
//             }
//         } catch (e) {
//             dispatch({
//                 type: UserActionTypes.SERVER_ERROR,
//                 payload: "Unknown error",
//             });
//             toast.error("Unknown error change");
//         }
//     };
// };
