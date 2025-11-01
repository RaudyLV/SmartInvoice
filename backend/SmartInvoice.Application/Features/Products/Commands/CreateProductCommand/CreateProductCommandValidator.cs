using FluentValidation;

namespace SmartInvoice.Application.Features.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters")
                .NotEmpty().WithMessage("Name is required");

            RuleFor(p => p.Description)
                    .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .PrecisionScale(10, 2, true).WithMessage("Invalid decimal format. Ex 10.00")
                .NotEmpty().WithMessage("Price is required");

            RuleFor(p => p.Stock)
                    .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
        }
    }
}