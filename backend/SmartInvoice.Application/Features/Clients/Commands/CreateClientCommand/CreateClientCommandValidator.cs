using System.Text.RegularExpressions;
using FluentValidation;

namespace SmartInvoice.Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            var phoneNumberFormatRegex = new Regex(@"^(809|829|849)-?([0-9]{3})-?([0-9]{4})$");
            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Invalid email format. Try again")
                .NotEmpty().WithMessage("Email is required");

            RuleFor(c => c.Address)
                .MaximumLength(200).WithMessage("Address cannot be greater than 200 characters");

            RuleFor(c => c.Name)
                .MaximumLength(100).WithMessage("Name cannot be greater than 100 characters")
                .NotEmpty().WithMessage("Name is required");

            RuleFor(c => c.Phone)
                .MaximumLength(12).WithMessage("Phone number exceeded 12 characters.")
                .Matches(phoneNumberFormatRegex).WithMessage("Invalid phone number format. Ex: [849, 829, 809]-000-0000 ")
                .NotEmpty().WithMessage("The phone number is required");

        }
    }
}