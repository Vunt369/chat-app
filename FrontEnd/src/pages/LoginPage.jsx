import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../Services/AuthService";
import { toast } from "react-toastify";
import { getUserFromToken } from "../utils/auth"; 
import useAuthStore from "../utils/useAuthStore";
const LoginPage = () => {
  const setUser = useAuthStore((state) => state.setUser); 
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

const handleSubmit = async (e) => {
  e.preventDefault();
  setLoading(true);

  try {
    const response = await login(email, password);

    if (response.data.isSuccess) {
      const token = response.data.data;
      localStorage.setItem("accessToken", token);

      const user = getUserFromToken();
      if (user) {
        setUser(user);
        toast.success("Đăng nhập thành công!");

        window.dispatchEvent(new Event("storage"));

        navigate("/");
      } else {
        toast.error("Không thể lấy thông tin người dùng!");
      }
    } else {
      toast.error("Đăng nhập thất bại! Kiểm tra lại thông tin.");
    }
  } catch (error) {
    toast.error("Đăng nhập thất bại! Vui lòng thử lại.");
  } finally {
    setLoading(false);
  }
};


  return (
    <div
      className="flex items-center justify-center min-h-screen bg-cover bg-center"
      style={{
        backgroundImage: "url('https://source.unsplash.com/1600x900/?chat')",
      }}
    >
      <div className="bg-white bg-opacity-80 p-8 rounded-lg shadow-lg w-96">
        <h2 className="text-center text-3xl font-bold text-gray-800 mb-6">
          Welcome Back!
        </h2>

        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <input
              type="email"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="mt-1 p-3 border border-gray-300 rounded-lg w-full"
              placeholder="Enter your email"
            />
          </div>

          <div className="mb-4">
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="mt-1 p-3 border border-gray-300 rounded-lg w-full"
              placeholder="Enter your password"
            />
          </div>

          <button
            type="submit"
            className="w-full bg-orange-500 text-white p-3 rounded-lg hover:bg-orange-700 transition"
          >
            {loading ? "Đang đăng nhập..." : "Login"}
          </button>
        </form>

        <Link to="/register">
          <p className="text-center text-gray-600 mt-4">
            Don't have an account?{" "}
          </p>
        </Link>
      </div>
    </div>
  );
};

export default LoginPage;
