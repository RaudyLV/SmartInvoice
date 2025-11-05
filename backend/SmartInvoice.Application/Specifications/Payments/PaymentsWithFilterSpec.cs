using Ardalis.Specification;
using SmartInvoice.Application.Common;
using SmartInvoice.Application.Dtos;
using SmartInvoice.Domain.Entities;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Specifications.Payments
{
    public class PaymentsWithFilterSpec : Specification<Payment, PaymentDto>
    {
        public PaymentsWithFilterSpec(
            string searchTerm = null!,
            int? minAmount = null,
            int? maxAmount = null,
            PaymentMethod? method = null,
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = "PaymentDate",
            bool sortDescending = false
        )
        {
            string normalizedSearch = StringNormalizerHelper.NormalizeSearchTerm(searchTerm);

            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    InvoiceNumber = p.Invoice.InvoiceNumber,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    Method = p.Method.ToString()
                });

            if (!string.IsNullOrEmpty(normalizedSearch))
            {
                Query.Search(x => x.Invoice.InvoiceNumber, $"%{normalizedSearch}%");
            }

            if (minAmount.HasValue && maxAmount.HasValue)
            {
                Query.Where(x => x.Amount >= minAmount && x.Amount <= maxAmount);
            }
            else if (minAmount.HasValue)
            {
                Query.Where(x => x.Amount >= minAmount);
            }
            else if (maxAmount.HasValue)
            {
                Query.Where(x => x.Amount <= minAmount);
            }

            if (method.HasValue)
            {
                Query.Where(x => x.Method == method);
            }
            
            if(sortDescending)
            {
                switch(sortBy?.ToLower())
                {
                    case "InvoiceNumber":
                        Query.OrderByDescending(x => x.Invoice.InvoiceNumber);
                        break;

                    case "Amount":
                        Query.OrderByDescending(x => x.Amount);
                        break;
                    
                    default:
                        Query.OrderByDescending(x => x.PaymentDate);
                        break;
                }
            }
            else
            {
                 switch(sortBy?.ToLower())
                {
                    case "InvoiceNumber":
                        Query.OrderBy(x => x.Invoice.InvoiceNumber);
                        break;

                    case "Amount":
                        Query.OrderBy(x => x.Amount);
                        break;
                    
                    default:
                        Query.OrderBy(x => x.PaymentDate);
                        break;
                }
            }
        }
    }
}