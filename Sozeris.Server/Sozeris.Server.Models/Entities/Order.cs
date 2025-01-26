using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Models.Entities;

public class Order
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
    public virtual Subscription Subscription { get; set; }
    public virtual Product Product { get; set; }
}