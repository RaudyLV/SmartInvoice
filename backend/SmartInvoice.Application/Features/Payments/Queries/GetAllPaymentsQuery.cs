using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Features.Payments.Queries
{
    public record GetAllPaymentsQuery : IRequest<Response<PagedList<PaymentDto>>>
    {
        public string SearchTerm { get; set; }
        public int? MinAmount { get; set; }
        public int? MaxAmount { get; set; }
        public PaymentMethod? Method { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "PaymentDate";
        public bool SortDescending { get; set; } = false;
    }

    public class GetAllPaymentsQueryHandler : 
    IRequestHandler<GetAllPaymentsQuery, Response<PagedList<PaymentDto>>>
    {
        private readonly IPaymentServices _paymentServices;

        public GetAllPaymentsQueryHandler(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        public async Task<Response<PagedList<PaymentDto>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var paymentsDto = await _paymentServices.PaymetsWithFilterAsync(request);

            int totalCount = await _paymentServices.CountAsync(request.SearchTerm);


            var pagedList = PagedList<PaymentDto>.Create(
                paymentsDto,
                totalCount,
                request.PageNumber,
                request.PageSize
            );

            return new Response<PagedList<PaymentDto>>(pagedList);
        }
    }
}