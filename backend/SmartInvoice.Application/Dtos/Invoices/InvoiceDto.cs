using SmartInvoice.Application.Dtos;


public class InvoiceDto
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<InvoiceItemDto> InvoiceItems { get; set; }
}