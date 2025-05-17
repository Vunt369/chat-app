# 💬 Chat App

A modern full-stack **Chat Application** with **User Authentication** and **Real-time Messaging** built using:

- ⚙️ Backend: ASP.NET Core Web API + SignalR
- 🖥 Frontend: ReactJS + TailwindCSS
- 🔐 Authentication: JWT-based login/register
- 🔁 Realtime: SignalR (WebSocket)

---

## 🚀 Features

- ✅ User registration & login with JWT authentication
- 🟢 Real-time messaging using SignalR
- 🧑‍🤝‍🧑 Online users list
- 📨 Chat history saved in database
- 🧾 Clean and responsive UI using TailwindCSS
- 🔐 Protected routes for authenticated users

---

## 🧱 Tech Stack

| Layer     | Technology               |
|-----------|---------------------------|
| Frontend  | ReactJS, TailwindCSS, Axios |
| Backend   | ASP.NET Core Web API, SignalR |
| Auth      | JWT (JSON Web Token)     |
| Database  | SQL Server               |
| Realtime  | SignalR + WebSocket      |
| Cache (optional) | Redis (for user presence or message cache) |

---

## 🖼️ Demo Screenshots

> *(You can insert images here, for example:)*

- Login/Register page  
- Real-time chat UI  
- Online users list  

---

## 🛠️ Installation & Setup

### 📦 Backend (ASP.NET Core)

```bash
cd server
dotnet restore
dotnet build
dotnet run
