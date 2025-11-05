using SmartInvoice.Application.Dtos.Invoices;
using SmartInvoice.Application.Features.Clients.Queries;
using SmartInvoice.Application.Features.Invoices.Queries;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Interfaces
{
    public interface IInvoiceServices
    {
        Task<List<InvoiceDto>> InvoicesWithFilterAsync(GetInvoicesWithFilterQuery query);
        Task<List<InvoiceDto>> GetClientInvoicesByFilter(GetClientInvoicesByFilterQuery query);
        Task<int> CountAsync(string query);
        Task UpdateInvoice(Invoice invoice);
        Task CreateInvoice(Invoice invoice);
        Task CancelInvoice(int invoiceId);
        Task<Invoice> GetById(int id);
    }
}