namespace Sozeris.Models.Entities;

public class Subscription
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    
    public decimal Price { get; set; }
    
    public List<Order> Orders { get; set; }

}