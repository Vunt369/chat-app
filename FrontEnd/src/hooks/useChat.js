import { useEffect, useRef, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { getAccessToken } from "../utils/auth";

const useChat = (
) => {
  const connectionRef = useRef(null);
  const [token, setToken] = useState(getAccessToken()); // Lấy token từ localStorage

  useEffect(() => {
    const startConnection = async () => {
    //   if (connectionRef.current) {
    //     await connectionRef.current.stop(); // Ngắt kết nối cũ trước khi tạo mới
    //     console.log("SignalR connection stopped.");
    //   }

      const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7060/chatHub", {
          accessTokenFactory: () => getAccessToken(), // Lấy token mới nhất
          transport: signalR.HttpTransportType.All,
        })
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();

      connectionRef.current = connection;
      try {
        await connection.start();
        console.log("SignalR Connected");

        connection.on("UpdateOnlineUsers", (users) => {
          console.log("Users Online:", users);
          setOnlineUsers(users);
        });
      } catch (error) {
        console.error("Error connecting to SignalR:", error);
      }
    };

    startConnection();

    return () => {
      if (
        connectionRef.current?.state === signalR.HubConnectionState.Connected
      ) {
        connectionRef.current.stop();
        console.log("SignalR connection stopped.");
      }
    };
  }, [token, hubUrl]); // 🔥 Thêm token vào dependency array

  // 🚀 Lắng nghe sự kiện thay đổi token để cập nhật lại SignalR
  useEffect(() => {
    const handleStorageChange = () => {
      setToken(getAccessToken());
    };
    window.addEventListener("storage", handleStorageChange);

    return () => {
      window.removeEventListener("storage", handleStorageChange);
    };
  }, []);

  return connectionRef;
};

export default useChat;
