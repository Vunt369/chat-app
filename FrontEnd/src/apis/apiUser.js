import axios from "axios";

const API_BASE_URL = "https://localhost:7060/api/User";

export const getUsers = async (token, value) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/search/${value}`,
         {
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


export const getUserDetailsByEmail = async (token,email) => {
  try {
    const response = await axios.get(`${API_BASE_URL}?email=${email}`, {
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
