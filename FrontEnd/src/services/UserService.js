
import { getUserProfile } from "../apis/apiAuth";
import { getUserDetailsByEmail, getUsers } from "../apis/apiUser";
import { getAccessToken } from "../utils/auth";

export const searchUser = async (value) => {
  try {
    var token = getAccessToken();
    const response = await getUsers(token, value);
    return response;
  } catch (error) {
    console.error("Error during mobile sign-up:", error);
    throw error;
  }
};
export const getProfile = async () => {
  try {
    var token = getAccessToken();
    const response = await getUserProfile(token);
    return response;
  } catch (error) {
    console.error("Error during mobile sign-up:", error);
    throw error;
  }
};
export const getUserDetails = async (email) => {
  try {
   
    var token = getAccessToken();
    const response = await getUserDetailsByEmail(token, email);
    return response;
  } catch (error) {
    console.error("Error during mobile sign-up:", error); 
    throw error;
  }
};
