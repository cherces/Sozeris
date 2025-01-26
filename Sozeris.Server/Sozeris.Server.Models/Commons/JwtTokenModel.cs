namespace Sozeris.Server.Models.Commons;

public class JwtTokenModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}