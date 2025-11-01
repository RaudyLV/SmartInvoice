using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInvoice.Application.Dtos
{
    public class InvoiceItemDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; } //impuesto aplicado
        public decimal Total { get; set; }

    }
}