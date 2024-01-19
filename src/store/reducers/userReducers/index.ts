import { UserState, UserActions, UserActionTypes } from "./types";

const initialState: UserState = {
    user: {},
    message: null,
    loading: false,
    error: null,
    isAuth: false,
    selectedUser: null,
    allUsers: [],
};

const UserReducer = (state = initialState, action: UserActions): UserState => {
    switch (action.type) {
        case UserActionTypes.START_REQUEST:

            return { ...state, loading: true };
        case UserActionTypes.LOGIN_USER_SUCCESS:
            return {
                ...state,
                isAuth: true,
                loading: false,
                user: action.payload.decodedToken,
                message: action.payload.message,
            };
        case UserActionTypes.FINISHED_REQUEST:
            return { ...state, loading: false };
        case UserActionTypes.LOGIN_USER_ERROR:
            return {
                ...state,
                loading: false,
                message: action.payload.message,
            };
        case UserActionTypes.LOGOUT_USER_SUCCESS:
            return {
                ...state,
                isAuth: false,
                loading: false,
                message: action.payload.message,
            };
        case UserActionTypes.LOGOUT_USER_ERROR:
            return {
                ...state,
                loading: false,
                message: action.payload.message,
            };
        case UserActionTypes.GET_ALL_USERS_SUCCESS:
            return {
                ...state,
                loading: false,
                allUsers: action.payload.allUsers,
            };
        case UserActionTypes.GET_ALL_USERS_ERROR:
            return {
                ...state,
                message: action.payload.message,
            };
        case UserActionTypes.ADD_USER_SUCCESS:

            return {
                ...state,
                loading: false,
                message: action.payload,
            };

        case UserActionTypes.ADD_USER_ERROR:
            return {
                ...state,
                loading: false,
                message: action.payload.message,
            };
        case UserActionTypes.GET_USER_SUCCESS:

            return {

                ...state,
                loading: false,
                selectedUser: action.payload,
            };

        case UserActionTypes.GET_USER_ERROR:
            return {
                ...state,
                loading: false,
                message: action.payload.message,
            };
        case UserActionTypes.EDIT_USER_SUCCESS:

            return {
                ...state,
                loading: false,
                message: action.payload,
            };

        case UserActionTypes.EDIT_USER_ERROR:
            return {
                ...state,
                loading: false,
                message: action.payload.message,
            };
        case UserActionTypes.DELETE_USER_SUCCESS:

            return {
                ...state,
                loading: false,
                message: action.payload,
            };

        case UserActionTypes.DELETE_USER_ERROR:
            return {
                ...state,
                loading: false,
                message: action.payload.message,
            };
        case UserActionTypes.CHANGE_PASSWORD_SUCCESS:

            return {
                ...state,
                loading: false,
                message: action.payload,
            };

        case UserActionTypes.CHANGE_PASSWORD_ERROR:
            return {
                ...state,
                loading: false,
                message: action.payload.message,
            };
        case UserActionTypes.SERVER_ERROR:
            return { ...state, loading: false };
        default:
            return state;
    }
};

export default UserReducer;
