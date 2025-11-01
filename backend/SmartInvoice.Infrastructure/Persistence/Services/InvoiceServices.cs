using SmartInvoice.Application.Exceptions;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Specifications.Invoices;
using SmartInvoice.Domain.Entities;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Infrastructure.Persistence.Services
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly IBaseRepository<Invoice> _repository;
        public InvoiceServices(IBaseRepository<Invoice> repository)
        {
            _repository = repository;
        }

        public async Task CancelInvoice(int invoiceId)
        {
            var invoice = await _repository.GetByIdAsync(invoiceId);
            if (invoice == null)
                throw new NotFoundException("Invoice not found");

            invoice.Status = Status.Cancelled;

            await UpdateInvoice(invoice);
        }

        public async Task CreateInvoice(Invoice invoice)
        {
            var (issueDate, dueDate) = GenerateInvoiceDates();
            invoice.Status = Status.Issued; //estado default
            invoice.CreatedAt = DateTime.UtcNow;
            invoice.IssueDate = issueDate;
            invoice.DueDate = dueDate;
            invoice.InvoiceNumber = await GenerateInvoiceNumber(); //ex: F000001.

            await _repository.AddAsync(invoice);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<InvoiceDto>> GetActivesInvoices()
        {
            var invoices = await _repository.ListAsync(new GetActiveInvoicesSpec());
            if (!invoices.Any())
                throw new NotFoundException("No active invoices were found");

            return invoices;
        }
        public async Task<List<InvoiceDto>> GetClientInvoicesByName(string clientName)
        {
            var clientInvoices = await _repository.ListAsync(new GetClientInvoicesSpec(clientName));
            if (clientInvoices == null || clientInvoices.Count <= 0)
                throw new NotFoundException("This client has no active invoices");

            return clientInvoices;
        }

        public async Task<Invoice> GetById(int id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            if (invoice == null)
                throw new NotFoundException("Invoice not found");

            return invoice;
        }

        public async Task UpdateInvoice(Invoice invoice)
        {
            await _repository.UpdateAsync(invoice);
            await _repository.SaveChangesAsync();
        }

        private (DateTime IssueDate, DateTime DueDate) GenerateInvoiceDates()
        {
            var random = new Random();
            DateTime issueDate = RandomDate(DateTime.UtcNow.AddYears(-1), DateTime.UtcNow);
            DateTime dueDate = issueDate.AddDays(random.Next(15, 61));
            return (issueDate, dueDate);
        }

        private DateTime RandomDate(DateTime start, DateTime end)
        {
            var random = new Random();
            int range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }

        private async Task<string> GenerateInvoiceNumber()
        {
            var invoices = await _repository.ListAsync();

            var lastInvoice = invoices
                                .OrderByDescending(i => i.Id)
                                .FirstOrDefault();

            int nextNumber = (lastInvoice?.Id ?? 0) + 1;

            return $"F{nextNumber:D6}"; // F000001, F000002, etc.
        }
    }
}