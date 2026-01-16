import axios from "axios";
import { useState } from "react";
import { AuthResponse } from "@/types";
import { signInUser, registerUser } from "@/services/authService";


export function useAuth() {
  const [user, setUser] = useState<AuthResponse | null>(() => {
    const userData = localStorage.getItem("user");
    return userData ? JSON.parse(userData) : null;
  });
  
  const [loading, setLoading] = useState(false);

  const login = async (email: string, password: string) => {
    setLoading(true);
    try {
      const res = await signInUser({ email, password });

      const userData = res.data;
      localStorage.setItem("token", res.data.token);
      localStorage.setItem("refreshToken", res.data.refreshToken);
      localStorage.setItem("user", JSON.stringify(userData));
      setUser(userData);
    } catch (err: any) {
      if (axios.isAxiosError(err)) {
        throw new Error(err.response?.data?.message || "Invalid credentials");
      }
      throw new Error("Error trying to sign in");
    } finally {
      setLoading(false);
    }
  };

  const register = async (
    userName: string,
    email: string,
    password: string,
    confirmPassword: string
  ) => {
    setLoading(true);
    try {
      await registerUser({ userName, email, password, confirmPassword });
    } catch (error) {
      if (axios.isAxiosError(error)) {
        throw new Error(error.response?.data?.message || "Error trying to sign up");
      }
      throw new Error("Error signing up");
    } finally {
      setLoading(false);
    }
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("user");
    setUser(null);
  };

  return {
    isAuthenticated: !!user,
    user,
    login,
    register,
    logout,
    loading,    
  };
};
