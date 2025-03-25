using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Models.Entities;

public class DeliveryHistory
{
    public int Id { get; set; }
    public int SubsctiptionId { get; set; }
    public DateTime DeliveryDate { get; set; }

    public virtual Subscription Subscription { get; set; }
}