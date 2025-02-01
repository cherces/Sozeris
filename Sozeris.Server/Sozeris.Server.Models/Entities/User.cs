using System.ComponentModel.DataAnnotations;
using Sozeris.Server.Models.Commons;

namespace Sozeris.Server.Models.Entities;

public class User : FilterBase
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    
    public virtual ICollection<Order> Orders { get; set; }
}