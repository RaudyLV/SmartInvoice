using Ardalis.Specification;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Application.Specifications.Invoices
{
    public class GetClientInvoicesSpec : Specification<Invoice, InvoiceDto>
    {
        public GetClientInvoicesSpec(string clientName)
        {
            Query.Search(x => x.Client.Name, "%" + clientName + "%")
                .Select(i => new InvoiceDto
                {
                    ClientName = i.Client.Name,
                    InvoiceNumber = i.InvoiceNumber,
                    IssueDate = i.IssueDate,
                    DueDate = i.DueDate,
                    InvoiceItems = i.InvoiceItems.Select(it => new InvoiceItemDto
                    {
                        ProductName = it.Product.Name,
                        Quantity = it.Quantity,
                        UnitPrice = it.UnitPrice,
                        TaxRate = it.TaxRate,
                        Total = it.Total
                    }).ToList(),
                    Status = i.Status.ToString(),
                    SubTotal = i.SubTotal,
                    TaxTotal = i.TaxTotal,
                    Discount = i.Discount,
                    Total = i.Total,
                    CreatedAt = i.CreatedAt
                });
        }
    }
}