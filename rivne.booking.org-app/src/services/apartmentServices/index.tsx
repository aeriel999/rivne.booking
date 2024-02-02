import http_common from '../../htt_common.ts';

import { getAccessToken } from '../userServices';

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

export async function getAll() {
  try {
    const data = await http_common.get("/api/Apartment/getAll", {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}
export async function deleteApartment(userId: number) {
  try {
    const data = await http_common.post("/api/Apartment/deleteApartment", userId, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function addApartment(model: any) {
  console.log("addApartment", model);
  try {
    const data = await http_common.post("/api/Apartment/addApartment", model, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });

    return data;

  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function getStreetsList() {
  try {
    const data = await http_common.get("/api/Apartment/getStreets", {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function getApartment(apartmentId: number) {
  try {
    const data = await http_common.get("/api/Apartment/getApartment?apartmentId=" + apartmentId, {
      headers: {
        "Content-Type": "application/json"
      }
    });
    return data;
  } catch (error: any) {
    return error.response.data.message;
  }
}

export async function editApartment(model: any) {
  console.log("addApartment", model);
  try {
    const data = await http_common.post("/api/Apartment/editApartment", model, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });

    return data;

  } catch (error: any) {
    console.log("data", error);
    return error.response.data.message;
  }
}