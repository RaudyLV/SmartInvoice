using SmartInvoice.Application.Dtos.Invoices;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Interfaces
{
    public interface IInvoiceServices
    {
        Task<List<InvoiceDto>> GetActivesInvoices();
        Task<List<InvoiceDto>> GetClientInvoicesByName(string clientName);
        Task UpdateInvoice(Invoice invoice);
        Task CreateInvoice(Invoice invoice);
        Task CancelInvoice(int invoiceId);
        Task<Invoice> GetById(int id);
    }
}