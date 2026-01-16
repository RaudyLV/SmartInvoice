import axios from "axios";
import { API_URL } from "@/config/api";
import { ApiResponse, CreateProductRequest, PagedRequest, PagedResponse, Product, UpdateProductRequest } from "@/types";

export async function getProductsPaged(request: PagedRequest) {
    const res = await axios.get<ApiResponse<PagedResponse<Product>>>(`${API_URL}/products`, {
        params: request
    });
    return res.data;    
}

export async function getProductById(id: number) {
    const res = await axios.get<ApiResponse<Product>>(`${API_URL}/products/${id}`);

    return res.data;
}

export async function createProduct(data: CreateProductRequest) {
    const res = await axios.post<ApiResponse<Product>>(`${API_URL}/products`, data)
    return res.data;
}

export async function updateProduct(id: number, data: UpdateProductRequest) {
    const res = await axios.put<ApiResponse<Product>>(`${API_URL}/products/${id}`, data);
    return res.data;
}

export async function deleteProduct(id: number) {
    const res = await axios.delete<ApiResponse<Product>>(`${API_URL}/products/${id}`);

    return res.data;
}