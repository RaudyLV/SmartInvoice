using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Payments.Queries
{
    public record GetAllPaymentsQuery : IRequest<Response<List<PaymentDto>>>;

    public class GetAllPaymentsQueryHandler : 
    IRequestHandler<GetAllPaymentsQuery, Response<List<PaymentDto>>>
    {
        private readonly IPaymentServices _paymentServices;

        public GetAllPaymentsQueryHandler(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        public async Task<Response<List<PaymentDto>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentServices.GetAllPaymentsAsync();

            return new Response<List<PaymentDto>>(payments);
        }
    }
}