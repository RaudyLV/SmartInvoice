
namespace SmartInvoice.Application.Dtos.Invoices
{
    public class CreateInvoiceItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}