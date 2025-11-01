using SmartInvoice.Domain.Enums;

namespace SmartInvoice.Application.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; }
    }
}