using Ardalis.Specification;
using SmartInvoice.Application.Common;

namespace SmartInvoice.Application.Specifications.Users
{
    public class CountUserSpec : Specification<User>
    {
        public CountUserSpec(string searchTerm = null!)
        {
            string normalizedSearch = StringNormalizerHelper.NormalizeSearchTerm(searchTerm);
            
            if(!string.IsNullOrEmpty(normalizedSearch))
            {
                Query.Search(x => x.Username, $"%{normalizedSearch}%");
            }
        }
    }
}