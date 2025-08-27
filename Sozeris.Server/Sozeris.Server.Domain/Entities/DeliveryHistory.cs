using System.ComponentModel.DataAnnotations;
using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Domain.Entities;

public class DeliveryHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubscriptionId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DeliveryStatus Status { get; set; }
    public string? Reason { get; set; }
    
    public User Courier { get; set; } = null!;
    public Subscription Subscription { get; set; } = null!;
}