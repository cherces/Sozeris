using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Models.Entities;

public class User : FilterBase
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    public string Role { get; set; }
    public string BearerToken { get; set; }
}