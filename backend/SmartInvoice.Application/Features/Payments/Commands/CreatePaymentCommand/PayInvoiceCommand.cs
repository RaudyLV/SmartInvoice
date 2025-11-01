using AutoMapper;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Entities;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Features.Payments.Commands
{
    public class PayInvoiceCommand : IRequest<Response<PaymentDto>>
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string PayMethod { get; set; }
        public PayInvoiceCommand(int invoiceId, decimal amount, string payMethod)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            PayMethod = payMethod;
        }
    }

    public class PayInvoiceCommandHandler : IRequestHandler<PayInvoiceCommand, Response<PaymentDto>>
    {
        private readonly IPaymentServices _paymentServices;
        private readonly IMapper _mapper;
        private readonly IInvoiceServices _invoiceServices;

        public PayInvoiceCommandHandler(IPaymentServices paymentServices, IMapper mapper, IInvoiceServices invoiceServices)
        {
            _paymentServices = paymentServices;
            _mapper = mapper;
            _invoiceServices = invoiceServices;
        }

        public async Task<Response<PaymentDto>> Handle(PayInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceServices.GetById(request.InvoiceId);

            if (invoice.Status == Status.Paid)
                throw new BadRequestException("This invoice has already been paid and cannot be modified.");

            if (invoice.Status == Status.Cancelled)
                throw new BadRequestException("This invoice has been cancelled and cannot be processed.");

            var (hasPayment, totalAmount) = await _paymentServices.HasPaymentAsync(request.InvoiceId, request.Amount);

            var payment = _mapper.Map<Payment>(request);
            await _paymentServices.PayInvoice(payment);

            var paymentDto = _mapper.Map<PaymentDto>(payment);
            paymentDto.InvoiceNumber = invoice.InvoiceNumber;

            if(hasPayment)
            {
                invoice.Status = Status.Paid;
                await _invoiceServices.UpdateInvoice(invoice);

                return new Response<PaymentDto>(paymentDto, $"Transaction complete. Change due: {totalAmount}");
            }

            return new Response<PaymentDto>(paymentDto, $"Partial payment received. Remaining balance: {totalAmount}");
        }
    }
}