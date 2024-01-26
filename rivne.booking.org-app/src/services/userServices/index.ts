import axios from "axios";
import http_common from '../../htt_common.ts';
import { ILogin, IProfileUser } from '../../interfaces/user';

http_common.interceptors.request.use(
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

http_common.interceptors.response.use(
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
          http_common.defaults.headers.common["Authorization"] =
            "Bearer " + accessToken;
          return http_common(originalConfig);
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
    const data = await http_common.post("/api/User/login", model, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

function refreshAccessToken() {
  return http_common.post("/RefreshToken", {
    token: getAccessToken(),
    refreshToken: getRefreshToken(),
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
    const data = await http_common.get("/api/User/logout?userId=" + userId, {
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

export async function updateUserProfile(model: IProfileUser) {
  try {
    const data = await http_common.post("/api/User/updateProfile", model, {
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
    const data = await http_common.get("/api/User/getAll", {
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
    const data = await http_common.post("/api/User/deleteUser", userId, {
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
    const data = await http_common.get("/api/User/getUser?userId=" + userId,{
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
    const data = await http_common.post("/api/User/editUser", user, {
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
    const data = await http_common.post("/api/User/addUser", user, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function addUserAvatar(model: any) {
  try {
    const data = await http_common.post("/api/User/addAvatar", model, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

//
// export async function editUser(user: any) {
//   const data = await User.editUser(user)
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
//
// export async function deleteUser(userId: string) {
//   const data = await User.deleteUser(userId)
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
//
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




