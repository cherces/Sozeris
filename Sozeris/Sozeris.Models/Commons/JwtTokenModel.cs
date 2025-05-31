using Sozeris.Models.Enums;

namespace Sozeris.Models;

public class JwtTokenModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}