import api from "@/config/axios";
import { API_URL } from "@/config/api";
import { ApiResponse, CreateInvoiceRequest, Invoice, PagedRequest, PagedResponse } from "@/types";


export async function getInvoicesPaged(request: PagedRequest) {
    const res = await api.get<ApiResponse<PagedResponse<Invoice>>>(`${API_URL}/invoices`, {
        params: request
    });

    return res.data;
}

export async function createInvoice(data: CreateInvoiceRequest) {
    const res = await api.post<ApiResponse<Invoice>>(`${API_URL}/invoices`, data);
    return res.data;
}

export async function updateInvoice(id: number) {
    const res = await api.put<ApiResponse<Invoice>>(`${API_URL}/invoices/${id}`);
    return res.data;
}
