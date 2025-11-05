using SmartInvoice.Application.Dtos;
using SmartInvoice.Application.Features.Payments.Queries;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Interfaces
{
    public interface IPaymentServices
    {
        Task<List<PaymentDto>> PaymetsWithFilterAsync(GetAllPaymentsQuery query);
        Task<int> CountAsync(string searchTerm);
        Task<(bool, decimal)> HasPaymentAsync(int invoiceId, decimal amount);
        Task PayInvoice(Payment payment);
        Task<PaymentDto> GetPaymentById(int id);
    }
}