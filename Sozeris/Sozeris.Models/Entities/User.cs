using Sozeris.Models.Enums;

namespace Sozeris.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public UserRole Role { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
}