using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Specifications.UsersRoles;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class UserRoleServices : IUserRoleServices
    {
        private readonly IBaseRepository<UserRole> _repository;
        private readonly IBaseRepository<Role> _rolesRepository;
        private readonly IUserServices _userServices;
        public UserRoleServices(IBaseRepository<UserRole> repository,
            IBaseRepository<Role> rolesRepository, IUserServices userServices)
        {
            _repository = repository;
            _rolesRepository = rolesRepository;
            _userServices = userServices;
        }

        public async Task AddToRoleAsync(User user, string rolename)
        {
            var existingUser = await _userServices.GetUserByIdAsync(user.Id);

            var existingRole = await _rolesRepository.FirstOrDefaultAsync(new GetRoleByNameSpec(rolename));
            if (existingRole == null)
            {
                throw new NotFoundException("Rol not found");
            }
            
            var userRole = new UserRole
            {
                UserId = existingUser.Id,
                RoleId = existingRole.Id
            };

            await _repository.AddAsync(userRole);
            await _repository.SaveChangesAsync();
        }
    }
}