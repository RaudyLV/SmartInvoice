using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Interfaces
{
    public interface IPaymentServices
    {
        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task<(bool, decimal)> HasPaymentAsync(int invoiceId, decimal amount);
        Task PayInvoice(Payment payment);
        Task<PaymentDto> GetPaymentById(int id);
    }
}