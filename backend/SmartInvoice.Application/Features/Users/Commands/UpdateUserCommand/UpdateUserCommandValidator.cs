
using FluentValidation;

namespace SmartInvoice.Application.Features.Users.Commands.UpdateUserCommand
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("The id is required");

            RuleFor(x => x.UserName)
                .MaximumLength(20).WithMessage("Username must not exceed 20 characters");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("A valid email is required");

          

        }
    }
}