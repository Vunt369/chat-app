import { useEffect, useState } from "react";
import LogoutButton from "../components/LogoutButton";



const Dashboard = () => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    // Giả lập lấy thông tin user từ token (sau này có thể fetch từ API)
    const token = localStorage.getItem("accessToken");
    if (!token) {
      window.location.href = "/login"; // Nếu chưa đăng nhập thì chuyển hướng
      return;
    }

    setUser({ name: "Người dùng 1", email: "user@example.com" }); // Giả lập user
  }, []);

  return (
    <div className="h-screen bg-gray-100 flex flex-col items-center justify-center">
      <div className="bg-white p-8 rounded-lg shadow-lg w-96 text-center">
        <h2 className="text-2xl font-bold mb-4">Xin chào, {user?.name}!</h2>
        <p className="text-gray-600">{user?.email}</p>

        <div className="mt-6">
            <button className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600">
              Bắt đầu trò chuyện
            </button>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
