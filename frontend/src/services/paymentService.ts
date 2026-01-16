import api from "@/config/axios";
import { API_URL } from "@/config/api";
import { ApiResponse, CreatePaymentRequest, PagedRequest, PagedResponse, Payment } from "@/types";

export async function getPaymentsPaged(request: PagedRequest) {
    const res = await api.get<ApiResponse<PagedResponse<Payment>>>(`${API_URL}/payments`, {
        params: request
    });

    return res.data;
}

export async function getPaymentById(id: number) {
    const res = await api.get<ApiResponse<Payment>>(`${API_URL}/payments/${id}`);
    return res.data;
}

export async function createPayment(data: CreatePaymentRequest) {
    const res = await api.post<ApiResponse<Payment>>(`${API_URL}/payments`, data);
    return res.data;
}