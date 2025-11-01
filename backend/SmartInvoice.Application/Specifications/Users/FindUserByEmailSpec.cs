
using Ardalis.Specification;
using SmartInvoice.Application.Dtos;

namespace SmartInvoice.Application.Specifications.Users
{
    public class FindUserByEmailSpec : Specification<User, UserDto>
    {
        public FindUserByEmailSpec(string email)
        {  
            Query.Where(user => user.Email == email) 
            .Select(user => new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                UserRoles = user.UserRoles.Select(x => x.Role.Name).ToList()
            });
        }
    }
}