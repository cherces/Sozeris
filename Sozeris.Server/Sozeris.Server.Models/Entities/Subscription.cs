using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Models.Entities;

public class Subscription
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public bool IsActive { get; set; }
}