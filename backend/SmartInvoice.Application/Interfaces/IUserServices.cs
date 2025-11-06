using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Features.Users.Queries;

namespace SmartInvoice.Application.Interfaces
{
    public interface IUserServices
    {
        Task<List<UserDto>> UsersWithFilterAsync(GetUsersWithFilterQuery query);
        Task<int> CountAsync(string searchTerm);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<UserDto> FindUserByEmailAsync(string email);
        Task UsernameOrEmailExistsAsync(string username, string email);
    }
}