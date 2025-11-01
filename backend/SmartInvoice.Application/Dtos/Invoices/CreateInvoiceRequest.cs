
namespace SmartInvoice.Application.Dtos.Invoices
{
    public class CreateInvoiceRequest
    {
        public int ClientId { get; set; }
        public List<CreateInvoiceItemRequest> Items { get; set; }
    }
}