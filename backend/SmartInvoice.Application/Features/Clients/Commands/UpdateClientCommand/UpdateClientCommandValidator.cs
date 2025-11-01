using System.Text.RegularExpressions;
using FluentValidation;

namespace SmartInvoice.Application.Features.Clients.Commands.UpdateClientCommand
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            var phoneNumberFormatRegex = new Regex(@"^(809|829|849)-?([0-9]{3})-?([0-9]{4})$");
            RuleFor(c => c.ClientId)
                .GreaterThan(0).WithMessage("Client id cannot be less than 0")
                .NotEmpty().WithMessage("Client id is required");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Invalid email format. Try again");

            RuleFor(c => c.Address)
                .MaximumLength(200).WithMessage("Address cannot be greater than 200 characters");

            RuleFor(c => c.Name)
                .MaximumLength(100).WithMessage("Name cannot be greater than 100 characters");

            RuleFor(c => c.Phone)
                .MaximumLength(12).WithMessage("Phone number exceeded 12 characters.")
                .Matches(phoneNumberFormatRegex).WithMessage("Invalid phone number format. Ex: [849, 829, 809]-000-0000 ");
        }
    }
}