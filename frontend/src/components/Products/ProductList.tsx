import SkeletonLoader from "../SkeletonLoader";
import { useMemo, useState } from "react";
import { SortIcon, SortField, SortOrder } from "./SortIcon";
import { Product, ModelListProps } from "@/types";
import { formatDate, capitalize } from "@/utils/helpers";
import { useProducts } from "@/hooks/useProducts";

const ProductList: React.FC<ModelListProps<Product>> = ({
  onEdit,
  onDelete,
}) => {
  const [sortField, setSortField] = useState<SortField>("createdAt");
  const [sortOrder, setSortOrder] = useState<SortOrder>("desc");
  const [searchTerm, setSearchTerm] = useState("");
  const {products, loading, error} = useProducts();

  if(loading){
    return <SkeletonLoader/>
  }
  if (error) {
    return <p className="text-red-500">{error}</p>;
  }

  const handleSort = (field: SortField) => {
    if (field === sortField) {
      setSortOrder(sortOrder === "asc" ? "desc" : "asc");
    } else {
      setSortField(field);
      setSortOrder("asc");
    }
  };

  const filteredAndSortedProducts = useMemo(() => {
    let filtered = products;

    if (searchTerm) {
      filtered = products.filter(
        (p) =>
          p.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
          p.description?.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    return [...filtered].sort((a, b) => {
      let aValue: any = a[sortField];
      let bValue: any = b[sortField];

      if (typeof aValue === "string") {
        aValue = aValue.toLowerCase();
        bValue = bValue.toLowerCase();
      }

      if (sortField === "createdAt") {
        aValue = new Date(aValue).getTime();
        bValue = new Date(bValue).getTime();
      }

      if (sortOrder === "asc") {
        return aValue > bValue ? 1 : -1;
      } else {
        return aValue < bValue ? 1 : -1;
      }
    });
  }, [products, sortField, sortOrder, searchTerm]);

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between gap-4">
        <div className="relative flex-1 max-w-md">
          <svg
            className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-gray-400"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
            />
          </svg>
          <input
            type="text"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Buscar productos..."
            className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none"
          />
        </div>
        <div className="text-sm text-gray-600">
          Mostrando{" "}
          <span className="font-semibold">
            {filteredAndSortedProducts.length}
          </span>{" "}
          de <span className="font-semibold">{products.length}</span> productos
        </div>
      </div>

      <div className="relative overflow-x-auto bg-white shadow-sm rounded-lg border border-gray-200">
        <table className="w-full text-sm text-left text-gray-600">
          <thead className="text-sm text-gray-700 bg-gray-50 border-b border-gray-200">
            <tr>
              <th scope="col" className="px-6 py-3 font-medium">
                <button
                  onClick={() => handleSort("name")}
                  className="flex items-center gap-2 hover:text-indigo-600 transition-colors"
                >
                  Producto
                  <SortIcon
                    field="name"
                    currentField={sortField}
                    order={sortOrder}
                  />
                </button>
              </th>

              <th scope="col" className="px-6 py-3 font-medium">
                <button
                  onClick={() => handleSort("price")}
                  className="flex items-center gap-2 hover:text-indigo-600 transition-colors"
                >
                  Precio
                  <SortIcon
                    field="price"
                    currentField={sortField}
                    order={sortOrder}
                  />
                </button>
              </th>

              <th scope="col" className="px-6 py-3 font-medium">
                <button
                  onClick={() => handleSort("createdAt")}
                  className="flex items-center gap-2 hover:text-indigo-600 transition-colors"
                >
                  Fecha
                  <SortIcon
                    field="createdAt"
                    currentField={sortField}
                    order={sortOrder}
                  />
                </button>
              </th>

              <th scope="col" className="px-6 py-3 font-medium">
                <span className="sr-only">Acciones</span>
              </th>
            </tr>
          </thead>
          <tbody>
            {filteredAndSortedProducts.length === 0 ? (
              <tr>
                <td
                  colSpan={6}
                  className="px-6 py-12 text-center text-gray-500"
                >
                  <div className="flex flex-col items-center gap-2">
                    <svg
                      className="w-12 h-12 text-gray-300"
                      fill="none"
                      stroke="currentColor"
                      viewBox="0 0 24 24"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth={2}
                        d="M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4"
                      />
                    </svg>
                    <p className="text-sm">No se encontraron productos</p>
                  </div>
                </td>
              </tr>
            ) : (
              filteredAndSortedProducts.map((product) => (
                <tr
                  key={product.id}
                  className="bg-white border-b border-gray-200 hover:bg-gray-50 transition-colors"
                >
                  <th
                    scope="row"
                    className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap"
                  >
                    <div>
                      <div className="font-semibold">{capitalize(product.name)}</div>
                      {product.description && (
                        <div className="text-xs text-gray-500 mt-0.5 line-clamp-1">
                          {product.description}
                        </div>
                      )}
                    </div>
                  </th>

                  <td className="px-6 py-4">
                    <span
                      className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
                        product.stock > 10
                          ? "bg-green-100 text-green-800"
                          : product.stock > 0
                          ? "bg-yellow-100 text-yellow-800"
                          : "bg-red-100 text-red-800"
                      }`}
                    >
                      {product.stock} unidades
                    </span>
                  </td>

                  <td className="px-6 py-4 font-semibold text-gray-900">
                    ${product.price.toFixed(2)}
                  </td>

                  <td className="px-6 py-4 text-gray-500 text-xs">
                    {formatDate(product.createdAt)}
                  </td>

                  <td className="px-6 py-4">
                    <div className="flex items-center gap-2">
                      {onEdit && (
                        <button
                          onClick={() => onEdit(product)}
                          className="text-indigo-600 hover:text-indigo-800 font-medium hover:underline transition-colors"
                        >
                          Editar
                        </button>
                      )}
                      {onDelete && (
                        <button
                          onClick={() => onDelete(product)}
                          className="text-red-600 hover:text-red-800 font-medium hover:underline transition-colors"
                        >
                          Eliminar
                        </button>
                      )}
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};
