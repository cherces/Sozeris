using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services;

public class AuthService : IAuthService
{
    private readonly string _secretKey;

    public AuthService(string secretKey)
    {
        _secretKey = secretKey;
    }
    
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: null,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}