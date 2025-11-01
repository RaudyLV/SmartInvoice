using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Specifications.Invoices
{
    public class GetActiveInvoicesSpec : Specification<Invoice, InvoiceDto>
    {
        public GetActiveInvoicesSpec()
        {
            Query.Where(i => i.Status == Status.Issued)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    InvoiceNumber = i.InvoiceNumber,
                    ClientName = i.Client.Name,
                    IssueDate = i.IssueDate,
                    DueDate = i.DueDate,
                    Status = i.Status.ToString(),
                    InvoiceItems = i.InvoiceItems.Select(it => new InvoiceItemDto
                    {
                        Id = it.Id,
                        InvoiceId = it.InvoiceId,
                        ProductName = it.Product.Name,
                        UnitPrice = it.UnitPrice,
                        Quantity = it.Quantity,
                        TaxRate = it.TaxRate,
                        Total = it.Total

                    }).ToList(),
                    SubTotal = i.SubTotal,
                    TaxTotal = i.TaxTotal,
                    Total = i.Total,
                    CreatedAt = i.CreatedAt

                });
        }
    }
}