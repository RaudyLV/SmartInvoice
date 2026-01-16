import api from "@/config/axios";
import { API_URL } from "@/config/api";
import { ApiResponse, PagedRequest, PagedResponse, UpdateUserRequest, User } from "@/types";

export async function getUsersPaged(request: PagedRequest) {
    const res = await api.get<ApiResponse<PagedResponse<User>>>(`${API_URL}/users`, {
        params: request
    });

    return res.data;
}

export async function getUserById(id: number) {
    const res = await api.get<ApiResponse<User>>(`${API_URL}/users/${id}`);
    return res.data;
}

export async function updateUser(id: number, data: UpdateUserRequest) {
    const res = await api.put<ApiResponse<User>>(`${API_URL}/users/${id}`, data);
    return res.data;
}

export async function deleteUser(id: number) {
    const res = await api.delete<ApiResponse<User>>(`${API_URL}/users/${id}`);
    return res.data;
}