using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.Commons;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtTokenRepository _jwtTokenRepository;

    public AuthService(JwtSettings jwtSettings, IJwtTokenRepository jwtTokenRepository)
    {
        _jwtSettings = jwtSettings;
        _jwtTokenRepository = jwtTokenRepository;
    }
    
    public string GenerateAccessTokenAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Issuer,
            claims: claims,
            expires: expiry,
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenAsync(int userId)
    {
        var refreshToken = Guid.NewGuid().ToString();
        await _jwtTokenRepository.AddRefreshTokenAsync(userId, refreshToken);
        return refreshToken;
    }

    public ClaimsPrincipal? ValidateAccessTokenAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Issuer
            }, out var validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}