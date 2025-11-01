
using FluentValidation;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Features.Payments.Commands.CreatePaymentCommand
{
    public class PayInvoiceCommandValidator : AbstractValidator<PayInvoiceCommand>
    {
        public PayInvoiceCommandValidator()
        {
            RuleFor(p => p.InvoiceId)
                .GreaterThan(0).WithMessage("Invoice id must be greater than 0")
                .NotEmpty().WithMessage("Invoice id is required");

            RuleFor(p => p.Amount)
                .PrecisionScale(10, 2, false)
                .GreaterThan(0).WithMessage("Amount must be greater than 0")
                .NotEmpty().WithMessage("Amount is required");

            RuleFor(p => p.PayMethod)
                .IsEnumName(typeof(PaymentMethod), false)
                .NotEmpty().WithMessage("Choose a pay method. Ex: cash, card, transfer");
        }
    }
}