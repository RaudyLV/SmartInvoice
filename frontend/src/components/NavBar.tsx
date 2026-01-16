import { useState } from "react";
import { Menu, X, Home, Mail, LogIn, UserPlus, LogOut } from "lucide-react";
import { useAuth } from "@/hooks/useAuth";
import Modal from "./Modal";

export default function NavBar({ onNavigate }) {
  const [isOpen, setIsOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const { isAuthenticated, logout, user } = useAuth();
  const [showLogoutModal, setShowLogoutModal] = useState(false);

  const toggleMenu = () => setIsOpen(!isOpen);
  const closeMenu = () => setIsOpen(false);

  const handleClick = (path: string) => {
    closeMenu();

    if (onNavigate) {
      onNavigate(path);
      setLoading(true);
    }
  };

  const handleLogoutClick = () => {
    setShowLogoutModal(true);
  };

  const handleConfirmLogout = () => {
    logout();
    closeMenu();
    handleClick("/");
  };

  return (
    <>
      <nav className="fixed top-0 left-0 right-0 z-50 bg-black/30 backdrop-blur-md border-b border-white/10">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex items-center justify-between h-16">
            {/* Logo */}
            <button
              onClick={() => handleClick("/")}
              className="flex items-center space-x-2 group"
            >
              <div className="w-10 h-10 bg-gradient-to-br from-purple-500 to-pink-500 rounded-lg flex items-center justify-center transform group-hover:scale-110 transition-transform duration-300">
                <span className="text-white font-bold text-xl">S</span>
              </div>
              <span className="text-white font-bold text-xl hidden sm:block">
                SmartInvoice
              </span>
            </button>

            {/* Desktop Menu */}
            <div className="hidden md:flex items-center space-x-1">
              <button
                onClick={() => handleClick("/")}
                className="flex items-center space-x-2 px-4 py-2 rounded-lg text-white/90 hover:text-white hover:bg-white/10 transition-all duration-300"
              >
                <Home size={18} />
                <span>Home</span>
              </button>
              <button
                onClick={() => handleClick("/contact")}
                className="flex items-center space-x-2 px-4 py-2 rounded-lg text-white/90 hover:text-white hover:bg-white/10 transition-all duration-300"
              >
                <Mail size={18} />
                <span>Contact</span>
              </button>
            </div>

            {/* Desktop Auth Buttons */}
            {isAuthenticated ? (
              <div className="hidden md:flex items-center space-x-3">
                <span>Hola! {user.userName}</span>
                <button
                  onClick={handleLogoutClick}
                  className="flex items-center px-4 py-2 gap-1 rounded-lg bg-red-500 peer-hover:bg-red-600 transform hover:scale-105 transition-all duration-300"
                >
                  Log out
                  <LogOut size={18} />
                </button>
              </div>
            ) : (
              <div className="hidden md:flex items-center space-x-3">
                <button
                  onClick={() => handleClick("/auth/signin")}
                  className="flex items-center space-x-2 px-4 py-2 rounded-lg text-white/90 hover:text-white hover:bg-white/10 transition-all duration-300 transform hover:scale-105"
                >
                  <LogIn size={18} />
                  <span>Sign In</span>
                </button>
                <button
                  onClick={() => handleClick("/auth/signup")}
                  className="flex items-center space-x-2 px-6 py-2 rounded-lg bg-gradient-to-r from-purple-500 to-pink-500 text-white font-medium hover:from-purple-600 hover:to-pink-600 transform hover:scale-105 transition-all duration-300 shadow-lg hover:shadow-purple-500/50"
                >
                  <UserPlus size={18} />
                  <span>Sign Up</span>
                </button>
              </div>
            )}

            {/* Mobile Menu Button */}
            <button
              onClick={toggleMenu}
              className="md:hidden p-2 rounded-lg text-white hover:bg-white/10 transition-colors duration-300"
              aria-label="Toggle menu"
            >
              {isOpen ? <X size={24} /> : <Menu size={24} />}
            </button>
          </div>
        </div>

        {/* Mobile Menu */}
        <div
          className={`md:hidden overflow-hidden transition-all duration-300 ease-in-out ${
            isOpen ? "max-h-96 opacity-100" : "max-h-0 opacity-0"
          }`}
        >
          <div className="px-4 pt-2 pb-4 space-y-2 bg-black/40 backdrop-blur-lg border-t border-white/10">
            <button
              onClick={() => handleClick("/")}
              className="w-full flex items-center space-x-3 px-4 py-3 rounded-lg text-white/90 hover:text-white hover:bg-white/10 transition-all duration-300 transform hover:translate-x-1"
            >
              <Home size={20} />
              <span className="font-medium">Home</span>
            </button>
            <button
              onClick={() => handleClick("/contact")}
              className="w-full flex items-center space-x-3 px-4 py-3 rounded-lg text-white/90 hover:text-white hover:bg-white/10 transition-all duration-300 transform hover:translate-x-1"
            >
              <Mail size={20} />
              <span className="font-medium">Contact</span>
            </button>

            {isAuthenticated ? (
               <div className="pt-4 border-t border-white/10 space-y-2">
                <button
                  onClick={handleLogoutClick}
                  className="w-full flex items-center justify-center space-x-2 px-4 py-3 rounded-lg text-white bg-white/10 hover:bg-white/20 transition-all duration-300 transform hover:scale-105"
                >
                  <LogOut/>
                  <span>Log out</span>
                </button>
               </div>
            ) : (
              <div className="pt-4 border-t border-white/10 space-y-2">
                <button
                  onClick={() => handleClick("/auth/signin")}
                  className="w-full flex items-center justify-center space-x-2 px-4 py-3 rounded-lg text-white bg-white/10 hover:bg-white/20 transition-all duration-300 transform hover:scale-105"
                >
                  <LogIn size={20} />
                  <span className="font-medium">Sign In</span>
                </button>
                <button
                  onClick={() => handleClick("/auth/signup")}
                  className="w-full flex items-center justify-center space-x-2 px-4 py-3 rounded-lg bg-gradient-to-r from-purple-500 to-pink-500 text-white font-medium hover:from-purple-600 hover:to-pink-600 transform hover:scale-105 transition-all duration-300 shadow-lg"
                >
                  <UserPlus size={20} />
                  <span>Sign Up</span>
                </button>
              </div>
            )}
          </div>
        </div>
      </nav>

      <Modal
        isOpen={showLogoutModal}
        onClose={() => setShowLogoutModal(false)}
        onConfirm={handleConfirmLogout}
        title="¿Cerrar sesión?"
        message="¿Estás seguro de que deseas cerrar sesión?"
        confirmText="Si, cerrar sesión"
        cancelText="Cancelar"
        type="danger"
      />
    </>
  );
}
