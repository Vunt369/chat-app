import { useState } from "react";

import useOnlineUsers from "../hooks/useOnlineUsers";
import OnlineUsers from "../components/User/OnlineUsers";
import ChatWindow from "../components/Chat/ChatWindow";

const ChatPage = () => {
  const [users, setUsers] = useState([]);
  const [selectedUser, setSelectedUser] = useState(null);
  useOnlineUsers(setUsers);
  return (
    <div className="flex h-screen">
      {/* Sidebar danh sách user online */}
      <div className="w-1/4 border-r border-gray-300">
        <OnlineUsers users={users} onSelectUser={setSelectedUser} />
      </div>

      {/* Cửa sổ chat */}
      <div className="w-3/4">
        {selectedUser ? (
          <ChatWindow user={selectedUser} />
        ) : (
          <div className="flex items-center justify-center h-full text-gray-500">
            Chọn một người để bắt đầu trò chuyện
          </div>
        )}
      </div>
    </div>
  );
};

export default ChatPage;
