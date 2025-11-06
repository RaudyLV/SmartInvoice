using Ardalis.Specification;
using SmartInvoice.Application.Common;
using SmartInvoice.Application.Dtos;

namespace SmartInvoice.Application.Specifications.Users
{
    public class UsersWithFilterSpec : Specification<User, UserDto>
    {
        public UsersWithFilterSpec(
            string searchTerm = null!,
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = null!,
            bool sortDescending = false
        )
        {
            string normalizedSearch = StringNormalizerHelper.NormalizeSearchTerm(searchTerm);

            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                Query.Search(x => x.Username, $"%{normalizedSearch}%");
            }

            if (sortDescending)
            {
                switch (sortBy?.ToLowerInvariant())
                {
                    case "Username":
                        Query.OrderByDescending(x => x.Username);
                        break;

                    default:
                        Query.OrderByDescending(x => x.CreatedAt);
                        break;
                }
            }
            else
            {
                switch(sortBy?.ToLowerInvariant())
                {
                    case "Username":
                        Query.OrderBy(x => x.Username);
                        break;

                    default:
                        Query.OrderBy(x => x.CreatedAt);
                        break;
                }
            }
        }   
    }
}