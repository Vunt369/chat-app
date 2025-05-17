import { Navigate, Outlet } from "react-router-dom";
import { getUserFromToken } from "../utils/auth";


const UserRoute = () => {
  const user = getUserFromToken();

  return user ? <Outlet /> : <Navigate to="/login" replace />;
};

export default UserRoute;
