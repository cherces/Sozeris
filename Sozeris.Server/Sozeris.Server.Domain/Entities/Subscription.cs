using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public User User { get; set; } = null!;
    public List<Order> Orders { get; set; } = new();
}