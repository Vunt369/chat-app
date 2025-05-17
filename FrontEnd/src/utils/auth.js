import { jwtDecode } from "jwt-decode";

export const getUserFromToken = () => {
  const token = localStorage.getItem("accessToken");
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    if (decoded.exp * 1000 < Date.now()) {
      localStorage.removeItem("accessToken");
      return null;
    }
    return decoded;
  } catch (error) {
    console.error("Lỗi giải mã token:", error);
    return null;
  }
};

export const getAccessToken = () => {
  const token = localStorage.getItem("accessToken");
  if (!token) return null;
  return token;
};