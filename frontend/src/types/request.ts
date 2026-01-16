export interface CreateInvoiceRequest{
    clientId: number,
    items: CreateInvoiceItemRequest[] 
}

interface CreateInvoiceItemRequest{
    productId: number,
    quantity: number
}

export interface RegisterUserRequest{
    userName: string,
    email: string,
    password: string,
    confirmPassword: string
}

export interface LogInUserRequest{
    email: string,
    password: string
}

export interface UpdateUserRequest{
    userName?: string | null,
    email?: string | null
}

export interface CreateProductRequest{
    name: string,
    description?: string | null,
    stock: number,
    price: number
}

export interface CreateProductRequest{
    name: string,
    description?: string | null,
    stock: number,
    price: number
}

export interface UpdateProductRequest{
    name?: string | null,
    description?: string | null,
    stock?: number | null,
    price?: number | null
}

export interface CreateClientRequest{
    name: string,
    email: string,
    phone: string,
    address?: string | null
}

export interface UpdateClientRequest{
    name?: string | null,
    email?: string | null,
    phone?: string | null,
    address?: string | null
}

export interface CreatePaymentRequest{
    invoiceNumber: string,
    amount: number,
    method: string
}

export interface ModelListProps<T>{
    onEdit?:(model: T) => void
    onDelete?:(model: T) => void
}