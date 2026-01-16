import { createClient, deleteClient, getClientInvoicesPaged, getClientsPaged, updateClient } from "@/services/clientService";
import { PagedRequest, Client, ApiResponse, PagedResponse, CreateClientRequest, UpdateClientRequest, Invoice } from "@/types";
import { useState, useEffect } from "react";

export async function useClients(initialRequest: PagedRequest) {
    const [client, setClient] = useState<Client[]>([]);
    const [invoice, setInvoice] = useState<Invoice[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() =>{
        fetchClients(initialRequest);
    }, [initialRequest]);

    async function fetchClients(request: PagedRequest) {
        try{
            setLoading(true);
            setError(null);

            const res: ApiResponse<PagedResponse<Client>> = await getClientsPaged(request);
            setClient(res.data.items);
        }catch(err: any){
            setError(err.message || "Error fetching clients");
        }finally{
            setLoading(false);
        }
    }

    async function fetchClientsInvoices(clientName: string) {
        try{
            setLoading(true);
            setError(null);

            const res: ApiResponse<PagedResponse<Invoice>> = await getClientInvoicesPaged(clientName);
            setInvoice(res.data.items);
        }catch(err: any){
            setError(err.message || "Error fetching client invoices");
        }finally{
            setLoading(false);
        }
    }

    async function addClient(data: CreateClientRequest) {
        try{
            const res = await createClient(data);
            setClient(prev => [...prev, res.data]);
        }catch(err: any){
            setError(err.message || "Error adding client");
        }
    }

    async function editClient(id: number, data: UpdateClientRequest) {
        try{
            const res = await updateClient(id, data);
            setClient(prev => prev.map(p => p.id === id ? res.data : p));
        }catch(err: any){
            setError(err.message || "Error updating client");
        }
    }

    async function removeClient(id: number) {
        try{
            const res = await deleteClient(id);
            setClient(prev => prev.filter(p => p.id !== id));
        }catch(err: any){
            setError(err.message || "Error deleting client");
        }
    }
    
}