import { UserActionTypes, UserActions } from "../../reducers/userReducers/types.ts";
import {
    login,
    logout,
    setAccessToken,
    setRefreshToken,
    removeTokens,
    updateUserProfile,
    getAll,
    deleteUser,
    addUser,
    getUser,
    editUser, addUserAvatar

    // changePassword,
} from '../../../services/userServices';
import { Dispatch } from "redux";
import { toast } from "react-toastify";
import jwtDecode from "jwt-decode";
import { ILogin, IProfileUser } from '../../../interfaces/user';

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

export const LogOut = (id: string) => {
    console.log("user.id", id)
    return async (dispatch: Dispatch<UserActions>) => {
        const data = await logout(id);

        if (data.data.success) {
            removeTokens();
            dispatch({
                type: UserActionTypes.LOGOUT_USER_SUCCESS,
                payload: data.data.message,
            });
        } else {
            dispatch({
                type: UserActionTypes.LOGOUT_USER_ERROR,
                payload: data.data.message,
            });
        }
    };
};

export const UpdateUserProfile = (model: IProfileUser) =>{
    return async (dispatch: Dispatch<UserActions>) => {
        try {
            dispatch({ type: UserActionTypes.START_REQUEST });

            const data = await updateUserProfile(model);

            if (!data.data.success) {
                dispatch({
                    type: UserActionTypes.UPDATE_USER_PROFILE_ERROR,
                    payload: data.data.message,
                });
               // toast.error(response.message);
            } else {
                dispatch({
                    type: UserActionTypes.UPDATE_USER_PROFILE_SUCCESS,
                    payload: data.data.message,
                  //  payload: {user : data.data.payLoad},
                });
               // toast.success(response.message);
            }
        } catch (e) {
            dispatch({
                type: UserActionTypes.SERVER_ERROR,
                payload: "Unknown error",
            });
          //  toast.error("Unknown edit error");
        }
    };
};

export const GetAll = () => {
    return async (dispatch: Dispatch<UserActions>) => {
        try {
            dispatch({ type: UserActionTypes.START_REQUEST });

            const data = await getAll();
            console.log("data.data.payLoad", data.data.payLoad);
            if (data.data.success) {
                dispatch({
                    type: UserActionTypes.GET_ALL_USERS_SUCCESS,
                    payload: data.data.payLoad ,
                });
            } else {
                dispatch({
                    type: UserActionTypes.GET_ALL_USERS_ERROR,
                    payload: data.data.message,
                });
            }
        } catch (error) {
            dispatch({
                type: UserActionTypes.SERVER_ERROR,
                payload: "Unknown error",
            });
           // toast.error("Unknown get error");
        }
    };
};

export const DeleteUser = (userId: string) => {
    return async (dispatch: Dispatch<UserActions>) => {
        try {
            dispatch({ type: UserActionTypes.START_REQUEST });

            const data = await deleteUser(userId);

            if (!data.data.success) {
                dispatch({
                    type: UserActionTypes.DELETE_USER_ERROR,
                    payload: data.data.message,
                });
                //toast.error(data.data.message);
            } else {
                dispatch({
                    type: UserActionTypes.DELETE_USER_SUCCESS,
                    payload: data.data.payLoad,
                });

               // toast.success(data.data.message);
            }
        } catch (e) {
            dispatch({
                type: UserActionTypes.SERVER_ERROR,
                payload: "Unknown error",
            });
            //toast.error("Unknown delete error");
        }
    };
};

export const GetUser = (userId: string) => {

    return async (dispatch: Dispatch<UserActions>) => {
        console.log("GetUser", userId);
        try {
            dispatch({ type: UserActionTypes.START_REQUEST });

            const data = await getUser(userId);

            if (!data.data.success) {
                dispatch({
                    type: UserActionTypes.GET_USER_ERROR,
                    payload: data.data.message,
                });
               // toast.error(response.message);
            } else {
                dispatch({
                    type: UserActionTypes.GET_USER_SUCCESS,
                    payload: data.data.payLoad,

                });
                console.log("try", data.data.payLoad);
               // toast.success(response.message);
            }
        } catch (e) {
            dispatch({
                type: UserActionTypes.SERVER_ERROR,
                payload: "Unknown error",
            });
         //   toast.error("Unknown user error");
        }
    };
};

export const EditUser = (user: any) => {
    return async (dispatch: Dispatch<UserActions>) => {
        try {
            dispatch({ type: UserActionTypes.START_REQUEST });

            const data = await editUser(user);

            if (!data.data.success) {
                dispatch({
                    type: UserActionTypes.EDIT_USER_ERROR,
                    payload: data.data.message,
                });
                //toast.error(response.message);
            } else {
                dispatch({
                    type: UserActionTypes.EDIT_USER_SUCCESS,
                    payload: data.data.message,
                });
               // toast.success(response.message);
            }
        } catch (e) {
            dispatch({
                type: UserActionTypes.SERVER_ERROR,
                payload: "Unknown error",
            });
            //toast.error("Unknown edit error");
        }
    };
};

export const AddUser = (user: any) => {
    return async (dispatch: Dispatch<UserActions>) => {
        try {
            dispatch({ type: UserActionTypes.START_REQUEST });

            const data = await addUser(user);

            if (!data.data.success) {
                dispatch({
                    type: UserActionTypes.ADD_USER_ERROR,
                    payload: data.data.message,
                });
                //toast.error(data.data.message);
            } else {
                dispatch({
                    type: UserActionTypes.ADD_USER_SUCCESS,
                    payload: data.data.message,
                });

              //  toast.success(data.data.message);
            }
        } catch (e) {
            dispatch({
                type: UserActionTypes.SERVER_ERROR,
                payload: "Unknown error",
            });
            toast.error("Unknown add error");
        }
    };
};

export const AddUserAvatar = (model: any) => {
    console.log("AddUserAvatar", model)
    return async (dispatch: Dispatch<UserActions>) => {
        try {
            dispatch({ type: UserActionTypes.START_REQUEST });

            const data = await addUserAvatar(model);

            if (!data.data.success) {
                dispatch({
                    type: UserActionTypes.ADD_USER_AVATAR_ERROR,
                    payload: data.data.message,
                });
                //toast.error(data.data.message);
            } else {
                dispatch({
                    type: UserActionTypes.ADD_USER_AVATAR_SUCCESS,
                    payload: data.data.message,
                });

                //  toast.success(data.data.message);
            }
        } catch (e) {
            dispatch({
                type: UserActionTypes.SERVER_ERROR,
                payload: "Unknown error",
            });
            toast.error("Unknown add error");
        }
    };
};


//


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
