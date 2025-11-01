using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public decimal TaxRate { get; set; } //Cantidad de impuestos aplicables
    public DateTime CreatedAt { get; set; }
}