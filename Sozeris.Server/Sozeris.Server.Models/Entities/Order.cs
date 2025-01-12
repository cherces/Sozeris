using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Models.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int SubscriptionId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}