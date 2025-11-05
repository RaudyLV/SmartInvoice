using Ardalis.Specification;
using SmartInvoice.Application.Common;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Specifications.Invoices
{
    public class InvoicesWithFilterSpec : Specification<Invoice, InvoiceDto>
    {
        public InvoicesWithFilterSpec(
            string searchTerm = null!,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            Status? status = null,
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = "CreatedAt",
            bool sortDescending = false)
        {
            string normalizedSearch = StringNormalizerHelper.NormalizeSearchTerm(searchTerm);

            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                Query.Search(x => x.InvoiceNumber, $"%{normalizedSearch}%")
                    .Search(x => x.Client.Name, $"%{normalizedSearch}%");
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

            if(status.HasValue)
            {
                Query.Where(x => x.Status == status);
            }

            if (sortDescending)
            {
                switch (sortBy?.ToLower())
                {
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
                switch(sortBy?.ToLower())
                {
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