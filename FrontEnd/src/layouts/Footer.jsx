    const Footer = () => {
      return (
        <footer className="bg-gray-900 text-gray-300 py-8">
          <div className="container mx-auto px-6 md:px-12">
            <div className="flex flex-col md:flex-row items-center justify-between">
              {/* Logo & Tên Ứng Dụng */}
              <div className="text-center md:text-left mb-4 md:mb-0">
                <h2 className="text-xl font-semibold text-white">Chat App</h2>
                <p className="text-sm text-gray-400">Kết nối bạn bè dễ dàng</p>
              </div>

              {/* Các liên kết */}
              <nav className="flex gap-6 text-sm">
                <a href="/about" className="hover:text-white transition">
                  Giới thiệu
                </a>
                <a href="/privacy" className="hover:text-white transition">
                  Chính sách bảo mật
                </a>
                <a href="/contact" className="hover:text-white transition">
                  Liên hệ
                </a>
              </nav>
            </div>

            <div className="text-center text-gray-500 text-sm mt-6 border-t border-gray-700 pt-4">
              © {new Date().getFullYear()} Chat App. All rights reserved.
            </div>
          </div>
        </footer>
      );
    };

    export default Footer;
