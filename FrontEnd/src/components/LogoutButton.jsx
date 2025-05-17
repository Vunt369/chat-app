import { useNavigate } from "react-router-dom";
import { signOutUser } from "../Services/AuthService";
import { toast } from "react-toastify";
import useAuthStore from "../utils/useAuthStore";

const LogoutButton = () => {
  const navigate = useNavigate();
  const { logout } = useAuthStore(); 

  const handleLogout = async () => {
    try {
      await signOutUser();
      localStorage.removeItem("accessToken");
      logout();
      toast.success("Bạn đã đăng xuất thành công");
      navigate("/");
    } catch (error) {
      toast.error("Đăng xuất thất bại!");
    }
  };

  return (
    <button
      onClick={handleLogout}
      className="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600 transition"
    >
      Đăng xuất
    </button>
  );
};

export default LogoutButton;
