import { useEffect, useState } from "react";
import ChatWindow from "../Chat/ChatWindow";
import useOnlineUsers from "../../hooks/useOnlineUsers";

const OnlineUsers = ({ onSelectUser }) => {
    const [onlineUsers, setOnlineUsers] = useState([]);
    const connectionRef = useOnlineUsers(setOnlineUsers);

      const fetchOnlineUsers = async () => {
        try {
          const response = await fetch(
            "https://localhost:7060/api/User/online-users"
          );
          const users = await response.json();
          console.log(users);
          setOnlineUsers(users);
        } catch (error) {
          console.error("Error fetching online users:", error);
        }
      };
        useEffect(() => {
          fetchOnlineUsers();
        }, []);
  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">🟢 Online Users</h2>
      {onlineUsers.length > 0 ? (
        <ul>
          {onlineUsers.map((user) => (
            <li
              key={user.email || user.id} // Tránh lỗi nếu email null
              className="p-2 cursor-pointer hover:bg-gray-100 rounded"
              onClick={() => onSelectUser && onSelectUser(user)}
            > 
              <div className="flex items-center space-x-3">
                <img
                  src={user.avartar || "/assets/default-avatar.png"}
                  alt={user.fullName || "User"}
                  className="w-10 h-10 rounded-full"
                />
                <span>{user.fullName || "Unknown User"}</span>
              </div>
            </li>
          ))}
        </ul>
      ) : (
        <p>No users online</p>
      )}
    </div>
  );
};

export default OnlineUsers;

