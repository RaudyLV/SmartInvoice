using System.Security.Cryptography;
using System.Text;
using MediatR;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Features.Payments.Queries
{
    public class GetAllPaymentsQuery : IRequest<Response<PagedList<PaymentDto>>>, ICacheableQuery
    {
        public string SearchTerm { get; set; }
        public int? MinAmount { get; set; }
        public int? MaxAmount { get; set; }
        public PaymentMethod? Method { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "PaymentDate";
        public bool SortDescending { get; set; } = false;

        public string CacheKey
        {
            get
            {
                var keyComponents = $"{SearchTerm}|{MinAmount}|{MaxAmount}|{Method}" +
                                    $"{PageNumber}|{PageSize}|{SortBy}|{SortDescending}";

                var sha256 = SHA256.Create();
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyComponents));
                var hash = Convert.ToBase64String(hashBytes)[..10];

                return $"payments_list_{hash}";
            }
        }
        public TimeSpan? CacheDuration => TimeSpan.FromMinutes(5); 
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