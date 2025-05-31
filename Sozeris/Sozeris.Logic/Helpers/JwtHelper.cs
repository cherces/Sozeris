using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Sozeris.Models.Enums;

namespace Sozeris.Logic.Helpers;

public class JwtHelper
{
    public static UserRole? ExtractUserRole(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(jwtToken))
            return null;

        var token = handler.ReadJwtToken(jwtToken);
        var roleClaim = token.Claims.FirstOrDefault(c => 
            c.Type == ClaimTypes.Role || c.Type == "role");

        if (roleClaim == null)
            return null;

        if (Enum.TryParse(typeof(UserRole), roleClaim.Value, true, out var role))
        {
            return (UserRole)role;
        }

        return null;
    }
}