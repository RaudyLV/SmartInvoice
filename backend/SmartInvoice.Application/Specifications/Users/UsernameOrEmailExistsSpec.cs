using Ardalis.Specification;
using SmartInvoice.Application.Dtos;

namespace SmartInvoice.Application.Specifications.Users
{
    public class UsernameOrEmailExistsSpec : Specification<User, UserDto>
    {
        public UsernameOrEmailExistsSpec(string username, string email)
        {
            Query.Where(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                        || p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}