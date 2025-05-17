import { useState } from 'react'
import './App.css'
import RegisterPage from './pages/RegisterPage'
import Dashboard from './pages/Dashboard'
import LoginPage from "./pages/LoginPage";
import HomePage from "./pages/HomePage";
import Header from './layouts/Header';
import { Route, Router, Routes } from 'react-router-dom';
import Footer from './layouts/Footer';
import GuestRoute from './routes/GuestRoute';
import UserRoute from './routes/UserRoute';
import UserProfile from './pages/User/ProfilePage';
import UserDetailsPage from './pages/User/UserDetailsPage';
import OnlineUsers from './components/User/OnlineUsers';
import { getAccessToken } from './utils/auth';
import ChatPage from './pages/ChatPage';

function App() {  
  const token = getAccessToken();
  const [user, setUser] = useState([]);
  
  return (
    <>
      <Header />
      <div className="min-h-screen">
        <Routes>
          <Route element={<GuestRoute />}>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
          </Route>
          <Route path="/" element={<HomePage />} />
          <Route path="/user/:email" element={<UserDetailsPage />} />

          <Route element={<UserRoute />}>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/profile" element={<UserProfile />} />
            <Route path="/chat" element={<ChatPage />} />
          </Route>
        </Routes>
      </div>
      {token && (
        <div>
          <OnlineUsers
            onSelectUser={(user) => console.log("Selected:", user)}
          />
        </div>
      )}
      <Footer />
    </>
  );
}

export default App
