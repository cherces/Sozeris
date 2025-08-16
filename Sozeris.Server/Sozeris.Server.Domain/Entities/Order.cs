using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sozeris.Server.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public Subscription Subscription { get; set; } = null!;
    public Product Product { get; set; } = null!;
}