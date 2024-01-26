export interface UserState {
  user: any;
  message: null | string;
  loading: boolean;
  error: null | string;
  isAuth: boolean;
  selectedUser: any;
  allUsers: [];
}

export enum UserActionTypes {
  START_REQUEST = 'START_REQUEST',
  FINISHED_REQUEST = 'FINISHED_REQUEST',
  LOGIN_USER_SUCCESS = 'LOGIN_USER_SUCCESS',
  LOGIN_USER_ERROR = 'LOGIN_USER_ERROR',
  SERVER_ERROR = 'SERVER_ERROR ',
  LOGOUT_USER_SUCCESS = 'LOGOUT_USER_SUCCESS',
  LOGOUT_USER_ERROR = 'LOGOUT_USER_ERROR',
  UPDATE_USER_PROFILE_SUCCESS = 'UPDATE_USER_PROFILE_SUCCESS',
  UPDATE_USER_PROFILE_ERROR = ' UPDATE_USER_PROFILE_ERROR',
  GET_ALL_USERS_SUCCESS = 'GET_ALL_USERS_SUCCESS',
  GET_ALL_USERS_ERROR = 'GET_ALL_USERS_ERROR',
  ADD_USER_SUCCESS = 'ADD_USER_SUCCESS',
  ADD_USER_ERROR = 'ADD_USER_ERROR',
  GET_USER_SUCCESS = 'GET_USER_SUCCESS',
  GET_USER_ERROR = 'GET_USER_ERROR',
  EDIT_USER_SUCCESS = 'EDIT_USER_SUCCESS',
  EDIT_USER_ERROR = 'EDIT_USER_ERROR',
  DELETE_USER_SUCCESS = 'DELETE_USER_SUCCESS',
  DELETE_USER_ERROR = 'DELETE_USER_ERROR',
  ADD_USER_AVATAR_SUCCESS = 'ADD_USER_AVATAR_SUCCESS',
  ADD_USER_AVATAR_ERROR = 'ADD_USER_AVATAR_ERROR',
  CHANGE_PASSWORD_SUCCESS = 'CHANGE_PASSWORD_SUCCESS',
  CHANGE_PASSWORD_ERROR = 'CHANGE_PASSWORD_ERROR',
}

interface StartRequestAction {
  type: UserActionTypes.START_REQUEST;
}

interface FinishRequestAction {
  type: UserActionTypes.FINISHED_REQUEST;
}

interface LoginUserSuccessAction {
  type: UserActionTypes.LOGIN_USER_SUCCESS;
  payload: any;
}

interface LoginUserErrorAction {
  type: UserActionTypes.LOGIN_USER_ERROR;
  payload: any;
}

interface ServerErrorAction {
  type: UserActionTypes.SERVER_ERROR;
  payload: any;
}

interface LogoutUserSuccessAction {
  type: UserActionTypes.LOGOUT_USER_SUCCESS;
  payload: any;
}

interface LogoutUserErrorAction {
  type: UserActionTypes.LOGOUT_USER_ERROR;
  payload: any;
}

interface UpdateUserProfileSuccessAction {
  type: UserActionTypes.UPDATE_USER_PROFILE_SUCCESS;
  payload: any;
}

interface UpdateUserProfileErrorAction {
  type: UserActionTypes.UPDATE_USER_PROFILE_ERROR;
  payload: any;
}

interface GetAllUsersSuccessAction {
  type: UserActionTypes.GET_ALL_USERS_SUCCESS;
  payload: any;
}

interface GetAllUsersErrorAction {
  type: UserActionTypes.GET_ALL_USERS_ERROR;
  payload: any;
}

interface AddUserSuccessAction {
  type: UserActionTypes.ADD_USER_SUCCESS;
  payload: any;
}

interface AddUserErrorAction {
  type: UserActionTypes.ADD_USER_ERROR;
  payload: any;
}

interface GetUserSuccessAction {
  type: UserActionTypes.GET_USER_SUCCESS;
  payload: any;
}

interface GetUserErrorAction {
  type: UserActionTypes.GET_USER_ERROR;
  payload: any;
}

interface EditUserSuccessAction {
  type: UserActionTypes.EDIT_USER_SUCCESS;
  payload: any;
}

interface EditUserErrorAction {
  type: UserActionTypes.EDIT_USER_ERROR;
  payload: any;
}

interface DeleteUserSuccessAction {
  type: UserActionTypes.DELETE_USER_SUCCESS;
  payload: any;
}

interface DeleteUserErrorAction {
  type: UserActionTypes.DELETE_USER_ERROR;
  payload: any;
}

interface AddUserAvatarSuccessAction {
  type: UserActionTypes.ADD_USER_AVATAR_SUCCESS;
  payload: any;
}

interface AddUserAvatarErrorAction {
  type: UserActionTypes.ADD_USER_AVATAR_ERROR;
  payload: any;
}

interface ChangePasswordSuccessAction {
  type: UserActionTypes.CHANGE_PASSWORD_SUCCESS;
  payload: any;
}

interface ChangePasswordErrorAction {
  type: UserActionTypes.CHANGE_PASSWORD_ERROR;
  payload: any;
}

export type UserActions =
  | StartRequestAction
  | FinishRequestAction
  | LoginUserSuccessAction
  | LoginUserErrorAction
  | ServerErrorAction
  | LogoutUserSuccessAction
  | LogoutUserErrorAction
  | GetAllUsersSuccessAction
  | GetAllUsersErrorAction
  | AddUserSuccessAction
  | AddUserErrorAction
  | GetUserSuccessAction
  | GetUserErrorAction
  | EditUserSuccessAction
  | EditUserErrorAction
  | DeleteUserSuccessAction
  | DeleteUserErrorAction
  | ChangePasswordSuccessAction
  | ChangePasswordErrorAction
  | UpdateUserProfileSuccessAction
  | UpdateUserProfileErrorAction
  | AddUserAvatarSuccessAction
  | AddUserAvatarErrorAction;
