import { useEffect, useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { getAccessToken } from "../utils/auth";

const useSignalR = (hubUrl, eventHandlers = {}) => {
  const [connection, setConnection] = useState(null);

  useEffect(() => {
    const token = getAccessToken();
    const newConnection = new HubConnectionBuilder()
      .withUrl(hubUrl, {
        accessTokenFactory: () => token,
      })
      .withAutomaticReconnect()
      .build();

    newConnection
      .start()
      .then(() => console.log("SignalR connected"))
      .catch((err) => console.error("Connection failed:", err));

    Object.entries(eventHandlers).forEach(([event, handler]) => {
      newConnection.on(event, handler);
    });

    setConnection(newConnection);

    return () => {
      Object.keys(eventHandlers).forEach((event) => {
        newConnection.off(event);
      });
      newConnection.stop();
    };
  }, [hubUrl]);

  return connection;
};

export default useSignalR;
