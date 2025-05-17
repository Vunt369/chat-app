import axios from "axios";

const API_BASE_URL = "https://localhost:7060/api/auth";
export const signIn = (email, password) => {
  return axios.post(
    `${API_BASE_URL}/login`,
    {
      email,
      password,
    },
    {
      headers: {
        accept: "*/*",
      },
    }
  );
};
export const signOut = async (token) => {
  return await axios.post(
    `${API_BASE_URL}/logout`,
    {},
    {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    }
  );
};
export const signUp = (userData) => {
  return axios.post(`${API_BASE_URL}/register`, userData, {
    headers: {
      "Content-Type": "application/json",
    },
  });
};
export const getUserProfile = async (token) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/profile`,{}, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error searching user:", error);
    throw error;
  }
};