using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Models.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}