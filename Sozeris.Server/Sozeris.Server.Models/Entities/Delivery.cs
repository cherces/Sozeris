using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Models.Entities;

public class Delivery
{
    [Key]
    public int Id { get; set; }
    public int SubsctiptionId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool IsDelivered { get; set; }
}