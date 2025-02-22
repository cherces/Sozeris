using Sozeris.Server.Models.Enums;

namespace Sozeris.Server.Models.DTO;

public class UserDTO
{    
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
}