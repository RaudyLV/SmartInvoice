using Ardalis.Specification;
using SmartInvoice.Application.Common;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Specifications.Invoices
{
    public class ClientsInvoicesWithFilterSpec : Specification<Invoice, InvoiceDto>
    {
        public ClientsInvoicesWithFilterSpec(
            string name = null!,
            DateTime? issuedDate = null,
            DateTime? dueDate = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            Status? status = null,
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = "CreatedAt",
            bool sortDescending = false
        )
        {
            string normalizedSearch = StringNormalizerHelper.NormalizeSearchTerm(name);

            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    ClientName = i.Client.Name,
                    InvoiceNumber = i.InvoiceNumber,
                    IssueDate = i.IssueDate,
                    DueDate = i.DueDate,
                    InvoiceItems = i.InvoiceItems.Select(it => new InvoiceItemDto
                    {
                        Id = it.Id,
                        InvoiceId = it.InvoiceId,
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
                }); ;

            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                Query.Search(x => x.Client.Name, $"%{normalizedSearch}%");
            }

            if (issuedDate.HasValue && dueDate.HasValue)
            {
                Query.Where(x => x.IssueDate == issuedDate.Value && x.DueDate == dueDate.Value);
            }
            else if (issuedDate.HasValue)
            {
                Query.Where(x => x.IssueDate == issuedDate.Value);
            }
            else if (dueDate.HasValue)
            {
                Query.Where(x => x.IssueDate == dueDate.Value);
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                Query.Where(x => x.Total >= minPrice.Value && x.Total <= maxPrice.Value);
            }
            else if (minPrice.HasValue)
            {
                Query.Where(x => x.Total >= minPrice.Value);
            }
            else if (maxPrice.HasValue)
            {
                Query.Where(x => x.Total <= maxPrice.Value);
            }
            
            if (status.HasValue)
            {
                Query.Where(x => x.Status == status);
            }

            if (sortDescending)
            {
                switch (sortBy?.ToLower())
                {
                    case "Total":
                        Query.OrderByDescending(x => x.Total);
                        break;

                    case "InvoiceNumber":
                        Query.OrderByDescending(x => x.InvoiceNumber);
                        break;


                    default:
                        Query.OrderByDescending(x => x.CreatedAt);
                        break;
                }
            }
            else
            {
                switch (sortBy?.ToLower())
                {
                    case "Total":
                        Query.OrderBy(x => x.Total);
                        break;

                    case "InvoiceNumber":
                        Query.OrderBy(x => x.InvoiceNumber);
                        break;

                    default:
                        Query.OrderBy(x => x.CreatedAt);
                        break;
                }
            }
        }
    }
}