using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
        public Invoice Invoice { get; set; }
    }
}