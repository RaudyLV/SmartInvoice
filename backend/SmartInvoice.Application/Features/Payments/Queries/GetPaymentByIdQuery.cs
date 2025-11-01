using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;

namespace SmartInvoice.Application.Features.Payments.Queries
{
    public class GetPaymentByIdQuery : IRequest<Response<PaymentDto>>
    {
        public int Id { get; set; }
        public GetPaymentByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Response<PaymentDto>>
    {
        private readonly IPaymentServices _paymentServices;

        public GetPaymentByIdQueryHandler(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        public async Task<Response<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentServices.GetPaymentById(request.Id);

            return new Response<PaymentDto>(payment);
        }
    }
}