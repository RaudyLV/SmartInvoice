using System.Data;
using FluentValidation;

namespace SmartInvoice.Application.Features.Clients.Commands.DeleteClientCommand
{
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(c => c.ClientId)
                .GreaterThan(0).WithMessage("Client id cannot be less than 0")
                .NotEmpty().WithMessage("Client id is required");
        }
    }
}