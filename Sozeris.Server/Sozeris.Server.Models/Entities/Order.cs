using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sozeris.Server.Models.Entities;

public class Order
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    [NotMapped]
    public decimal CalcPrice => Product.Price * Quantity;
    
    public virtual Subscription Subscription { get; set; }
    public virtual Product Product { get; set; }
}