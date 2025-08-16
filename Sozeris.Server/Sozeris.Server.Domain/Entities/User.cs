using System.ComponentModel.DataAnnotations;
using Sozeris.Server.Domain.Commons;
using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}