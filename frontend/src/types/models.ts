import { InvoiceStatus, PayMethod } from "./enums"

export interface Product{
    id: number,
    name: string,
    description: string,
    price: number,
    stock: number,
    taxRate: number,
    createdAt: Date 
}

export interface User{
    id: number,
    name: string,
    email: string,
    createdAt: Date,
    roles: string[]
}

export interface Client{
    id: number,
    name: string,
    email: string,
    phone: string,
    address: string,
    createdAt: Date,
    invoices: Invoice[]
} 

export interface Invoice{
    id: number,
    clientName: string,
    invoiceNumber: string,
    issueDate: Date,
    dueDate: Date,
    status: InvoiceStatus,
    subTotal: number,
    taxTotal: number,
    total: number,
    createdAt: Date
    invoiceItems: InvoiceItem[]
}

export interface InvoiceItem{
    id: number,
    invoiceNumber: string,
    productName: string,
    quantity: number,
    unitPrice: number,
    taxRate: number,
    total: number
}

export interface Payment{
    id: number,
    invoiceNumber: string,
    amount: number,
    paymentDate: Date,
    method: PayMethod
}


