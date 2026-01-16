import { createProduct, deleteProduct, getProductById, getProductsPaged, updateProduct } from "@/services/productService";
import { PagedRequest, PagedResponse, Product, ApiResponse, CreateProductRequest, UpdateProductRequest} from "@/types";
import { useState, useEffect } from "react";

export function useProducts(initialRequest: PagedRequest = {pageNumber: 1, pageSize: 10}){
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetchProducts(initialRequest)
    }, [initialRequest]);

    async function fetchProducts(request: PagedRequest) {
        try{
            setLoading(true);
            setError(null);

            const res: ApiResponse<PagedResponse<Product>> = await getProductsPaged(request);
            setProducts(res.data.items);
        }catch(err: any){
            setError(err.message || "Error trying to fetch products");
        }finally{
            setLoading(false);
        }
    }

    async function fetchProductById(id: number) {
        try{
            const res: ApiResponse<Product> = await getProductById(id);
            return res.data;
        }catch(err: any){
            setError(err.message || "Error trying to fetch the product")
        }
    }

    async function addProduct(data: CreateProductRequest) {
        try{
            const res = await createProduct(data);
            setProducts(prev => [...prev, res.data]);
        }catch(err: any){
            setError(err.message || "Error trying to add the product");
        }
    }

    async function editProduct(id: number, data: UpdateProductRequest) {
        try{
            const res = await updateProduct(id, data);
            setProducts(prev => prev.map(p => p.id === id ? res.data : p));
        }catch(err: any){
            setError(err.message || "Error trying to update the product");
        }
    }

    async function removeProduct(id: number) {
        try{
            await deleteProduct(id);
            setProducts(prev => prev.filter(p => p.id !== id));
        }catch(err: any){
            setError(err.message || "Error trying to delete the product");
        }
    }

    return {
        products,
        loading,
        error,
        addProduct,
        editProduct,
        removeProduct,
        fetchProductById,
        fetchProducts
    };
}

