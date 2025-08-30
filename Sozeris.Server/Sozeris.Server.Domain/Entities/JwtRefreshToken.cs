namespace Sozeris.Server.Domain.Entities;

public class JwtRefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public string? Device { get; set; }
    public bool IsRevoked { get; set; }
    
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => !IsRevoked && !IsExpired;
    
    public User User { get; set; } = null!;
}