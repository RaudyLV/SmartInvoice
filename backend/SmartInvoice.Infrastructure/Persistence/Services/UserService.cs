using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Features.Users.Queries;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Specifications.Users;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class UserService : IUserServices
    {
        private readonly IBaseRepository<User> _repository;

        public UserService(IBaseRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task CreateUserAsync(User user)
        {
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            await _repository.DeleteAsync(user);
            await _repository.SaveChangesAsync();
        }

        public async Task<UserDto> FindUserByEmailAsync(string email)
        {
            var user = await _repository.FirstOrDefaultAsync(new FindUserByEmailSpec(email));

            return user!;
        }

        public async Task<List<UserDto>> UsersWithFilterAsync(GetUsersWithFilterQuery query)
        {
            var users = await _repository.ListAsync(new UsersWithFilterSpec(
                searchTerm: query.SearchTerm,
                pageNumber: query.PageNumber,
                pageSize: query.PageSize,
                sortBy: query.SortBy,
                sortDescending: query.SortDescending
            ));

            if (users.Count <= 0)
            {
                throw new NotFoundException("There is no active users");
            }

            return users;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _repository.FirstOrDefaultAsync(new GetUserByIdSpec(userId));
            if (user == null)
            {
                throw new NotFoundException($"User not found.");
            }

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _repository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found");
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;

            await _repository.UpdateAsync(existingUser);
        }

        public async Task UsernameOrEmailExistsAsync(string username, string email)
        {
            var existingUser = await _repository.FirstOrDefaultAsync(new UsernameOrEmailExistsSpec(username, email));
            if (existingUser != null)
            {
                throw new BadRequestException("Username or email already exists");
            }
        }

        public async Task<int> CountAsync(string searchTerm)
        {
            int totalCount = await _repository.CountAsync(new CountUserSpec(searchTerm));

            return totalCount;
        }
    }
}