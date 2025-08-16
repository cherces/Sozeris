namespace Sozeris.Server.Domain.Entities;

public class JwtRefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
}