import { API_URL } from "@/config/api";
import api from "@/config/axios";
import { ApiResponse, AuthResponse, LogInUserRequest, RegisterUserRequest} from "@/types";

export async function registerUser(data: RegisterUserRequest) {
    const res = await api.post<ApiResponse<string>>(`${API_URL}/auth/signup`, data);
    return res.data;
}

export async function signInUser(data: LogInUserRequest) {
    const res = await api.post<ApiResponse<AuthResponse>>(`${API_URL}/auth/signin`, data);
    return res.data;
}
