using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Models.Models;

public class Delivery
{
    public int SubscriptionId { get; set; }
    public string Address { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
    public string? Reason { get; set; }
    public List<DeliveryItem> Items { get; set; } = new();
}

public class DeliveryItem
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}