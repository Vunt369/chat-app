import React from "react";
import { useNavigate } from "react-router-dom";


const HomePage = () => {
  const navigate = useNavigate();

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Banner */}
      <div className="relative w-full h-[400px] md:h-[500px] lg:h-[600px]">
        <img
          src="https://images.unsplash.com/photo-1542744095-291d1f67b221"
          alt="Banner"
          className="w-full h-full object-cover brightness-75"
        />
        <div className="absolute inset-0 flex flex-col items-center justify-center text-white text-center px-4">
          <h1 className="text-3xl md:text-5xl font-bold drop-shadow-lg leading-tight">
            Welcome to Our Chat App
          </h1>
          <p className="text-base md:text-lg mt-2 max-w-lg">
            Connect with friends and chat in real-time.
          </p>
          <button
            onClick={() => navigate("/dashboard")}
            className="mt-6 px-6 py-3 bg-blue-500 text-white text-lg font-semibold rounded-full shadow-lg hover:bg-blue-600 transition-all duration-300"
          >
            Get Started
          </button>
        </div>
      </div>

      {/* Poster Section */}
      <div className="container mx-auto py-12 px-6">
        <h2 className="text-2xl md:text-3xl font-semibold text-center mb-8 text-gray-800">
          Explore Our Features
        </h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          {[
            {
              img: "https://images.unsplash.com/photo-1611606063065-ee7946f0787a?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y2hhdCUyMGFwcHxlbnwwfHwwfHx8MA%3D%3D",
              title: "Instant Messaging",
            },
            {
              img: "https://images.unsplash.com/photo-1529333166437-7750a6dd5a70",
              title: "Group Chat",
            },
            {
              img: "https://images.unsplash.com/photo-1517245386807-bb43f82c33c4",
              title: "Secure Communication",
            },
          ].map((item, index) => (
            <div
              key={index}
              className="group relative overflow-hidden rounded-xl shadow-lg"
            >
              <img
                src={item.img}
                alt={item.title}
                className="w-full h-48 md:h-56 object-cover transform group-hover:scale-105 transition duration-300"
              />
              <div className="absolute inset-0 bg-gray-300 bg-opacity-50 flex items-center justify-center opacity-0 group-hover:opacity-100 transition duration-300">
                <p className="text-white text-lg font-semibold">{item.title}</p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>  
  );
};

export default HomePage;
