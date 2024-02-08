import axios from "axios";

import { ILogin, IProfileUser } from '../../interfaces/user';

import {APP_ENV} from "../../env";

const instance = axios.create({
  baseURL: APP_ENV.BASE_URL + "/api/User",
  headers: {
    "Content-Type": "application/json",
  },
});
instance.interceptors.request.use(
  (config: any) => {
    const token = getAccessToken();
    if (token) {
      config.headers["Authorization"] = "Bearer " + token;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

instance.interceptors.response.use(
  (res) => {
    return res;
  },
  async (err) => {
    const originalConfig = err.config;
    if (err.response) {
      // Validation failed, ...
      if (err.response.status === 400 && err.response.data) {
        return Promise.reject(err.response.data);
      }
      // Access Token was expired
      if (
        err.response.status === 401 &&
        !originalConfig._retry &&
        getAccessToken() != null
      ) {
        originalConfig._retry = true;
        try {
          const rs = await refreshAccessToken();
          const { accessToken, refreshToken } = rs.data;
          setRefreshToken(refreshToken);
          setAccessToken(accessToken);
          instance.defaults.headers.common["Authorization"] =
            "Bearer " + accessToken;
          return instance(originalConfig);
        } catch (_error: any) {
          if (_error.response && _error.response.data) {
            return Promise.reject(_error.response.data);
          }
          return Promise.reject(_error);
        }
      }
      if (err.response.status === 403 && err.response.data) {
        return Promise.reject(err.response.data);
      }
      if (err.response.status === 404) {
        if (axios.isAxiosError(err)) {
          return Promise.reject(err.response.data);
        }
        return;
      }
    }
    return Promise.reject(err);
  }
);

export async function login(model: ILogin) {
  try {
    const data = await instance.post("/login", model, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export function refreshAccessToken() {

  return instance.post("/RefreshToken", {
    token: getAccessToken(),
    refreshToken: getRefreshToken()}, {
    headers: {
      "Content-Type": "application/json"
    }
  });
}

export function setAccessToken(token: string) {
  window.localStorage.setItem("accessToken", token);
}

export function setRefreshToken(token: string) {
  window.localStorage.setItem("refreshToken", token);
}

export function getAccessToken(): null | string {
  const token = window.localStorage.getItem("accessToken");
  return token;
}

export function getRefreshToken(): null | string {
  const token = window.localStorage.getItem("refreshToken");
  return token;
}

export async function logout(userId: string) {
  try {
    const data = await instance.get("/logout?userId=" + userId, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export function removeTokens() {
  window.localStorage.removeItem("accessToken");
  window.localStorage.removeItem("refreshToken");
}

export async function confirmEmail(userId : string, token: string) {
  try {
    const data = await instance.get("/confirmemail", {
      params: {
        userId: userId,
        token: token,
      },
      headers: {
        "Content-Type": "application/json",
      },
    });
    return data;
  } catch (error : any) {
    return error.response.data.message;
  }
}




export async function updateUserProfile(model: IProfileUser) {
  try {
    const data = await instance.post("/updateProfile", model, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function getAll() {
  try {
    const data = await instance.get("/getAll", {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function deleteUser(userId: string) {
  try {
    const data = await instance.post("/deleteUser", userId, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }

}

export async function getUser(userId: string) {
  try {
    const data = await instance.get("/getUser?userId=" + userId,{
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function editUser(user: any) {
  try {
    const data = await instance.post("/editUser", user, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function addUser(user: any) {
  try {
    const data = await instance.post("/addUser", user, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function addUserAvatar(file: any) {
  try {

    const data = await instance.post("/addAvatar", file, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}


// export async function changePassword(passModel: any) {
//   const data = await User.changePassword(passModel)
//     .then((response) => {
//       return {
//         response,
//       };
//     })
//     .catch((error) => {
//       return error.response;
//     });
//
//   return data;
// }




