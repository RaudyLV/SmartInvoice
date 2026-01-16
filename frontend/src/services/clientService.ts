import { API_URL } from "@/config/api";
import api from "@/config/axios";
import { ApiResponse, Client, CreateClientRequest, Invoice, PagedRequest, PagedResponse, UpdateClientRequest } from "@/types";

export async function getClientsPaged(request: PagedRequest) {
    const res = await api.get<ApiResponse<PagedResponse<Client>>>(`${API_URL}/clients`,{
        params: request
    });

    return res.data;
}
export async function getClientInvoicesPaged(clientName: string) {
    const res = await api.get<ApiResponse<PagedResponse<Invoice>>>(`${API_URL}/clients/${clientName}/invoices`,{
        params: clientName
    });

    return res.data;
}

export async function getClientById(id: number) {
    const res = await api.get<ApiResponse<Client>>(`${API_URL}/clients/${id}`);
    return res.data;
}

export async function createClient(data: CreateClientRequest) {
    const res = await api.post<ApiResponse<Client>>(`${API_URL}/clients`, data);
    return res.data;
}

export async function updateClient(id: number, data: UpdateClientRequest){
    const res = await api.put<ApiResponse<Client>>(`${API_URL}/clients/${id}`, data);
    return res.data;
}

export async function deleteClient(id: number) {
    const res = await api.delete<ApiResponse<Client>>(`${API_URL}/clients/${id}`);
    return res.data;
}




