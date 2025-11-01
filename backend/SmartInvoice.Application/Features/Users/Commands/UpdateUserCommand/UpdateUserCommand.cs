using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Users.Commands.UpdateUserCommand
{
    public class UpdateUserCommand : IRequest<Response<UserDto>>
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;

        public UpdateUserCommandHandler(IMapper mapper, IUserServices userServices)
        {
            _mapper = mapper;
            _userServices = userServices;
        }

        public async Task<Response<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userServices.UsernameOrEmailExistsAsync(request.UserName!, request.Email!);

            var existingUserDto = await _userServices.GetUserByIdAsync(request.Id); //Entidad existente sin actualizar

            var user = _mapper.Map<User>(existingUserDto); //mapeo a user

            _mapper.Map(request, user); //mapeamos la request con ese user 

            await _userServices.UpdateUserAsync(user);

            var userDto = _mapper.Map<UserDto>(user); //mapeamos a dto el user actualizado

            return new Response<UserDto>(userDto, "User updated successfully");
        }
    }
}