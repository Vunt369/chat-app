import { Link } from "react-router-dom";
import LogoutButton from "../components/LogoutButton";
import SearchBar from "../components/User/SearchBar";
import useAuthStore from "../utils/useAuthStore";

const Header = () => {
  const { user } = useAuthStore();

  return (
    <header className="flex items-center justify-between p-4 shadow-md bg-black w-full">
      {/* Logo */}
      <Link to="/">
        <h1 className="text-xl font-bold text-white">Chat App</h1>
      </Link>

      {user && (
        <div className="flex-grow flex justify-center">
          <SearchBar />
        </div>
      )}

      <div className="flex items-center gap-4">
        {user ? (
          <>
            <Link to="/profile">
              <span className="text-white font-medium">
                Xin chào, {user.email}
              </span>
            </Link>
            <LogoutButton />
          </>
        ) : (
          <div className="flex gap-4">
            <Link to="/login">
              <button className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 transition">
                Login
              </button>
            </Link>
            <Link to="/register">
              <button className="bg-green-500 text-white px-4 py-2 rounded-lg hover:bg-green-600 transition">
                Signup
              </button>
            </Link>
          </div>
        )}
      </div>
    </header>
  );
};

export default Header;
