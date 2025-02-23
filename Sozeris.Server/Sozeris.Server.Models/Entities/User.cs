using System.ComponentModel.DataAnnotations;
using Sozeris.Server.Models.Commons;
using Sozeris.Server.Models.Enums;

namespace Sozeris.Server.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    
    public virtual ICollection<Subscription> Subscriptions { get; set; }
}