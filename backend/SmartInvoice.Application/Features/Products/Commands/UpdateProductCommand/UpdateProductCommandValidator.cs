
using FluentValidation;

namespace SmartInvoice.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");

            RuleFor(p => p.Description)
                    .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .PrecisionScale(10, 2, true).WithMessage("Invalid decimal format. Ex 10.00");

            RuleFor(p => p.Stock)
                    .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
        }
    }
}