using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using SmartInvoice.Application.Dtos;


namespace SmartInvoice.Application.Specifications.Users
{
    public class GetUserByIdSpec : Specification<User, UserDto>
    {
        public GetUserByIdSpec(int userId)
        {
            Query.Where(u => u.Id == userId)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    UserRoles = user.UserRoles.Select(ur => string.Join(",", ur.Role.Name)).ToList()
                });

        }
    }
}