namespace Sozeris.Models.Entities;

public class Order
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}