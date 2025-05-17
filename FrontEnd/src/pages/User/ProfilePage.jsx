import { useEffect, useState } from "react";
import { getProfile } from "../../services/UserService";

const UserProfile = () => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [editMode, setEditMode] = useState(false);
  useEffect(() => {
    const fetchUser = async () => {
      try {
        const profile = await getProfile();
        setUser(profile.data);
      } catch (error) {
        console.error("Lỗi khi lấy profile:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchUser();
  }, []);
  if (loading) return <p className="text-center mt-10">Đang tải...</p>;
  if (!user) return <p className="text-center mt-10">Bạn chưa đăng nhập!</p>;

  return (
    <div className="max-w-4xl mx-auto mt-10 p-6 bg-white shadow-lg rounded-lg">
      <div className="flex items-center gap-6">
        <img
          src={user.avartar || "https://via.placeholder.com/150"}
          alt="Avatar"
          className="w-24 h-24 rounded-full border-4 border-gray-300"
        />
        {/* Thông tin user */}
        <div>
          <h2 className="text-2xl font-semibold">
            {user.fullName || "User Name"}
          </h2>
          <p className="text-gray-500">{user.email}</p>
          <p className="text-gray-600">
            Giới tính:{" "}
            <span className="font-medium">
              {user.gender || "Không xác định"}
            </span>
          </p>
        </div>
      </div>

      {/* Chỉnh sửa thông tin */}
      <div className="mt-6">
        <button
          className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition"
          onClick={() => setEditMode(!editMode)}
        >
          {editMode ? "Hủy" : "Chỉnh sửa hồ sơ"}
        </button>
      </div>

      {/* Form chỉnh sửa */}
      {editMode && (
        <form className="mt-4 space-y-4">
          <input
            type="text"
            placeholder="Họ và Tên"
            className="w-full p-2 border rounded-md"
            defaultValue={user.fullName}
          />
          <input
            type="email"
            placeholder="Email"
            className="w-full p-2 border rounded-md"
            defaultValue={user.email}
            disabled
          />
          <select
            className="w-full p-2 border rounded-md"
            defaultValue={user.gender}
          >
            <option value="Male">Nam</option>
            <option value="Female">Nữ</option>
            <option value="Other">Khác</option>
          </select>
          <button className="w-full bg-green-500 text-white py-2 rounded-md hover:bg-green-600 transition">
            Lưu thay đổi
          </button>
        </form>
      )}
    </div>
  );
};

export default UserProfile;
