export interface ApiResponse<T>{
    succeeded: boolean,
    message: string,
    errors: string[],
    data: T
}

export interface PagedResponse<T>{
    items: T[],
    pageNumber: number
    pageSize: number,
    totalCount: number,
    totalPages: number,
    hasPreviousPage: boolean,
    hasNextPage: boolean
}

export interface PagedRequest {
    pageNumber?: number;
    pageSize?: number;
    search?: string;
    sortBy?: string;
    sortDirection?: "asc" | "desc";
}

export interface AuthResponse{
    id: number,
    userName: string,
    email: string,
    roles: string[],
    isAuthenticated: boolean,
    token: string,
    refreshToken: string
}
