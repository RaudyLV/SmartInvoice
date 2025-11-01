using SmartInvoice.Application.Dtos;

namespace SmartInvoice.Application.Interfaces
{
    public interface IUserServices
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int userId);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<UserDto> FindUserByEmailAsync(string email);
        Task UsernameOrEmailExistsAsync(string username, string email);
    }
}