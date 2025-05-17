
import { toast } from "react-toastify";
import { signIn, signOut, signUp } from "../apis/apiAuth";


export const login = async (email, password) => {
  try {
    const response = await signIn(email, password);
    return response;
  } catch (error) {
    console.error("Login failed", error);
    toast.error("Đăng nhập thất bại");
    throw error;
  }
};
export const signOutUser = async () => {
  try {
    const token = localStorage.getItem("accessToken");
    const response = await signOut(token);
    return response;
  } catch (error) {
    console.error("Error during sign-out:", error);
    throw error;
  }
};

export const signUpUser = async (userData) => {
  try {
    const response = await signUp(userData);
    return response;
  } catch (error) {
    console.error("Error during mobile sign-up:", error);
    throw error;
  }
};

