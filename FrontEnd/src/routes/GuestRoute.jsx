import { Navigate, Outlet } from "react-router-dom";
import { getUserFromToken } from "../utils/auth";


const GuestRoute = () => {
  const user = getUserFromToken();

  return user ? <Navigate to="/dashboard" replace /> : <Outlet />;
};

export default GuestRoute;
