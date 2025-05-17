import React, { useState, useRef, useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";

import { Link } from "react-router-dom";
import { searchUser } from "../../services/UserService";

const SearchBar = () => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState([]);
  const searchRef = useRef(null);

  // Handle search input
  const handleSearch = async (e) => {
    const value = e.target.value;
    setQuery(value);
    if (value.trim()) {
      try {
        const response = await searchUser(value);
        setResults(response.data || []);
      } catch (error) {
        console.error("Error searching products:", error);
        setResults([]);
      }
    } else {
      setResults([]);
    }
  };

  // Handle click outside
  useEffect(() => {
    const handleClickOutside = (e) => {
      if (searchRef.current && !searchRef.current.contains(e.target)) {
        setResults([]);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  return (
    <div className="relative z-50 w-full sm:w-1/3 p-2 mx-auto" ref={searchRef}>
      {/* Search Input */}
      <div className="flex w-full bg-white border-2 border-orange-500 rounded-full px-4 py-2 items-center shadow-sm">
        <input
          className="flex-grow bg-transparent outline-none placeholder-gray-400"
          placeholder={"Tìm kiếm người dùng..."}
          type="text"
          value={query}
          onChange={handleSearch}
        />
        <FontAwesomeIcon
          icon={faMagnifyingGlass}
          className="text-orange-500 font-medium cursor-pointer"
        />
      </div>
      {results.length > 0 && (
        <div className="absolute z-40 w-full bg-white border border-gray-300 rounded-lg shadow-lg mt-2 overflow-hidden max-h-60 overflow-y-auto">
          {results.map((user) => (
            <Link
              to={`/user/${user.email}`}
              className="block p-3 hover:bg-gray-100 transition-colors"
            >
              <div className="flex items-center space-x-4">
                {/* Product Image with fallback */}
                <img
                  src={user.avartar || "/assets/react.svg"}
                  alt={user.fullName || "User"}
                  className="w-12 h-12 object-cover rounded"
                  onError={(e) => (e.target.src = "/assets/react.svg")}
                />
                <span className="text-gray-800 font-medium">
                  {user.fullName}
                </span>
              </div>
            </Link>
          ))}
        </div>
      )}
    </div>
  );
};

export default SearchBar;
