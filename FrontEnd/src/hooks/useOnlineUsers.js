import { useEffect, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import { getAccessToken } from "../utils/auth";

const useOnlineUsers = (
  setOnlineUsers,
  hubUrl = "https://localhost:7060/chatHub"
) => {
  const connectionRef = useRef(null);

  useEffect(() => {
    const startConnection = async () => {

      if (connectionRef.current) {
        await connectionRef.current.stop(); // Ngắt kết nối cũ trước khi tạo mới
        console.log("🔌 SignalR connection stopped.");
      }

      const connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl, {
          accessTokenFactory: () => getAccessToken(), // Luôn lấy token mới nhất
          transport: signalR.HttpTransportType.All,
        })
        .withAutomaticReconnect()
        .build();

      connectionRef.current = connection;

      try {
        await connection
          .start()
          .then(console.log("✅ SignalR Connected!"))
          .catch((err) => console.log("Connection failed: ", err));
   
        // connection
        //   .invoke("GetOnlineUsers")
        //   .catch((err) => console.error("Invoke Error:", err));

        connection.on("UserOnline", (user) => {
          // console.log("User",user  ) 
        })

        connection.on("UpdateOnlineUsers", (users) => {
          console.log("👥 Users Online:", users);
          
          if (setOnlineUsers)  setOnlineUsers(users);
        });
      } catch (error) {
        console.error("❌ Error connecting to SignalR:", error);
      }
    };

    startConnection();

    return () => {
      if (
        connectionRef.current?.state === signalR.HubConnectionState.Connected
      ) {
        connectionRef.current.stop();
        console.log("🛑 SignalR connection stopped.");
      }
    };
  }, [hubUrl]);

  return connectionRef;
};

export default useOnlineUsers;
