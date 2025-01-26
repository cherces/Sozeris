namespace Sozeris.Server.Models.Entities;

public class JwtRefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
}