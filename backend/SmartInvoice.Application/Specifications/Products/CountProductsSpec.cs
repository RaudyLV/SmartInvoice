using Ardalis.Specification;

namespace SmartInvoice.Application.Specifications.Products
{
    public class CountProductsSpec : Specification<Product>
    {
        public CountProductsSpec(string searchTerm = null!)
        {
            if(!string.IsNullOrEmpty(searchTerm))
            {
                Query.Search(x => x.Name, $"%{searchTerm}%")
                    .Search(x => x.Description, $"%{searchTerm}%");
            }
        }
    }
}