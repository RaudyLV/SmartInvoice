using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Features.Payments.Queries;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Specifications.Payments;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IBaseRepository<Payment> _baseRepository;
        private readonly IInvoiceServices _invoiceServices;

        public PaymentServices(IBaseRepository<Payment> baseRepository, IInvoiceServices invoiceServices)
        {
            _baseRepository = baseRepository;
            _invoiceServices = invoiceServices;
        }

        public async Task<List<PaymentDto>> PaymetsWithFilterAsync(GetAllPaymentsQuery query)
        {
            var payments = await _baseRepository.ListAsync(new PaymentsWithFilterSpec(
                searchTerm: query.SearchTerm,
                minAmount: query.MinAmount,
                maxAmount: query.MaxAmount,
                method: query.Method,
                pageNumber: query.PageNumber,
                pageSize: query.PageSize,
                sortBy: query.SortBy,
                sortDescending: query.SortDescending
            ));

            if (!payments.Any())
            {
                throw new NotFoundException("No payments have been made yet.");
            }

            return payments;
        }

        public async Task<PaymentDto> GetPaymentById(int id)
        {
            var payment = await _baseRepository.FirstOrDefaultAsync(new GetPaymentByIdSpec(id));
            if (payment == null)
                throw new NotFoundException("Payment not found");

            return payment;
        }

        public async Task PayInvoice(Payment payment)
        {
            await _invoiceServices.GetById(payment.InvoiceId);

            payment.PaymentDate = DateTime.UtcNow;
            await _baseRepository.AddAsync(payment);
            await _baseRepository.SaveChangesAsync();
        }
        public async Task<int> CountAsync(string searchTerm)
        {
            int totalCount = await _baseRepository.CountAsync(new CountPaymentsSpec(searchTerm));

            return totalCount;
        }

        public async Task<(bool, decimal)> HasPaymentAsync(int invoiceId, decimal amount)
        {
            var invoicePayments = await _baseRepository.ListAsync(new GetLastInvoicePayment(invoiceId));

            decimal totalSum = invoicePayments.Sum(x => x.Amount) + amount;

            var (result, change) = await VerifyAmount(invoiceId, totalSum);

            return (result, change);
        }

        private async Task<(bool, decimal)> VerifyAmount(int invoiceId, decimal amount)
        {
            var invoice = await _invoiceServices.GetById(invoiceId);
            decimal change = 0;
    
            if (amount < invoice.Total)
            {
                change = invoice.Total - amount;
                return (false, change);
            }

            if (amount > invoice.Total) 
            {
                change = amount - invoice.Total;
                return (true, change);
            }

            return (true, change); //amount == total
        }

    }
}