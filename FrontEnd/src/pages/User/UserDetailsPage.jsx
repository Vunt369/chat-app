import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getUserDetails } from "../../services/UserService";

const UserDetailsPage = () => {
  const { email } = useParams();
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const profile = await getUserDetails(email);
        setUser(profile);
        // Giả sử API có trả về danh sách bài viết
        setPosts(profile.posts || []);
      } catch (error) {
        console.error("Lỗi khi lấy profile:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchUser();
  }, [email]);

  if (loading) return <p className="text-center mt-10">Đang tải...</p>;
  if (!user)
    return <p className="text-center mt-10">Không tìm thấy người dùng!</p>;

  return (
    <div className="max-w-4xl mx-auto mt-10 bg-white shadow-lg rounded-lg">
      {/* Ảnh bìa */}
      <div className="relative">
        <img
          src={
            user.coverImg ||
            "https://t3.ftcdn.net/jpg/08/75/76/00/360_F_875760029_1m8KXDYOQe9HgliqdLaFW2BGe7Po0GaZ.jpg"
          }
          alt="Cover"
          className="w-full h-48 object-cover rounded-t-lg"
        />
        {/* Avatar + Tên */}
        <div className="absolute bottom-0 left-6 transform translate-y-1/2 flex items-center">
          <img
            src={user.avartar || "https://via.placeholder.com/150"}
            alt="Avatar"
            className="w-24 h-24 rounded-full border-4 border-white shadow-md"
          />
          <div className="ml-4">
            <h2 className="text-2xl font-bold text-white bg-black bg-opacity-50 px-2 rounded">
              {user.fullName || "Người dùng"}
            </h2>
            <p className="text-gray-300 bg-black bg-opacity-50 px-2 rounded">
              {user.email}
            </p>
          </div>
        </div>
      </div>

      {/* Phần thông tin user */}
      <div className="px-6 pt-16 pb-6">
        <p className="text-gray-600">
          <span className="font-medium">Giới tính:</span>{" "}
          {user.gender || "Không xác định"}
        </p>
        <p className="text-gray-600">
          <span className="font-medium">Số điện thoại:</span>{" "}
          {user.phone || "Chưa cập nhật"}
        </p>
        <p className="text-gray-600">
          <span className="font-medium">Địa chỉ:</span>{" "}
          {user.address || "Chưa có địa chỉ"}
        </p>
      </div>

      {/* Nút tương tác */}
      <div className="flex items-center gap-4 px-6">
        <button className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition">
          Nhắn tin
        </button>
        <button className="px-4 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400 transition">
          Kết bạn
        </button>
      </div>

      {/* Danh sách bài viết */}
      <div className="px-6 mt-6">
        <h3 className="text-xl font-bold text-gray-800">Bài viết</h3>
        {posts.length > 0 ? (
          <div className="space-y-4 mt-4">
            {posts.map((post, index) => (
              <div
                key={index}
                className="p-4 border rounded-md shadow-sm bg-gray-100"
              >
                <p className="text-gray-800">{post.content}</p>
                <p className="text-sm text-gray-500 mt-2">{post.createdAt}</p>
              </div>
            ))}
          </div>
        ) : (
          <p className="text-gray-500 mt-4">Chưa có bài viết nào.</p>
        )}
      </div>
    </div>
  );
};

export default UserDetailsPage;
