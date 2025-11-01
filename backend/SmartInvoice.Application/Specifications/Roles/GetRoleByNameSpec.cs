using Ardalis.Specification;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Roles
{
    public class GetRoleByNameSpec : Specification<Role>
    {
        public GetRoleByNameSpec(string name)
        {
            Query.Where(x => x.Name == name);
        }
    }
}