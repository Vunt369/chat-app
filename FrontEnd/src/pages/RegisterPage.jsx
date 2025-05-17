import { useState } from "react";

import { signUpUser } from "../Services/AuthService";

const RegisterPage = () => {
  const [userData, setUserData] = useState({
    email: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (e) => {
    setUserData({ ...userData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (userData.password !== userData.confirmPassword) {
      toast.error("Mật khẩu không khớp!");
      return;
    }
    try {
      await signUpUser(userData);
      toast.success("Đăng ký thành công!");
    } catch (error) {
      toast.error("Đăng ký thất bại!");
    }
  };

  return (
    <div className="flex justify-center items-center h-screen bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-lg w-96">
        <h2 className="text-2xl font-bold mb-4 text-center">Đăng ký</h2>
        <form onSubmit={handleSubmit} className="space-y-4">
          <input
            type="email"
            name="email"
            placeholder="Email"
            className="w-full p-2 border border-gray-300 rounded-lg"
            onChange={handleChange}
          />
          <input
            type="password"
            name="password"
            placeholder="Mật khẩu"
            className="w-full p-2 border border-gray-300 rounded-lg"
            onChange={handleChange}
          />
          <input
            type="password"
            name="confirmPassword"
            placeholder="Nhập lại mật khẩu"
            className="w-full p-2 border border-gray-300 rounded-lg"
            onChange={handleChange}
          />
          <button
            type="submit"
            className="w-full bg-green-500 text-white p-2 rounded-lg hover:bg-green-600"
          >
            Đăng ký
          </button>
        </form>
      </div>
    </div>
  );
};

export default RegisterPage;
