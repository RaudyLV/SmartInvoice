using FluentValidation;

namespace SmartInvoice.Application.Features.Invoices.Commands.CreateInvoicesCommand
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(x => x.Request.ClientId)
                    .GreaterThan(0).WithMessage("Cliend id must be greater than 0")
                    .NotEmpty().WithMessage("Client id is required");

            RuleForEach(x => x.Request.Items).ChildRules(i =>
            {
                i.RuleFor(i => i.ProductId)
                    .GreaterThan(0).WithMessage("Product id must be greater than 0")
                    .NotEmpty().WithMessage("Product id is required");
                
                i.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Product id must be greater than 0")
                    .NotEmpty().WithMessage("Product id is required");
            });
        }
    }
}