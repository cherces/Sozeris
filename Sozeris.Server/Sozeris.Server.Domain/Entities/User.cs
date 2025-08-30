using System.ComponentModel.DataAnnotations;
using Sozeris.Server.Domain.Commons;
using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Salt { get; set; } = String.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public string Phone { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public bool IsActive { get; set; }

    public List<JwtRefreshToken> JwtRefreshTokens { get; set; } = new();
}