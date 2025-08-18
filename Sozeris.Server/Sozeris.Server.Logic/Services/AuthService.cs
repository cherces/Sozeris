using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sozeris.Server.Domain.Interfaces.Services;
using Sozeris.Server.Domain.Commons;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;

namespace Sozeris.Server.Logic.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtTokenRepository _jwtTokenRepository;
    private readonly IUserRepository _userRepository;

    public AuthService(IOptions<JwtSettings> jwtSettings, IJwtTokenRepository jwtTokenRepository, IUserRepository userRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _jwtTokenRepository = jwtTokenRepository;
        _userRepository = userRepository;
    }

    public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string login, string password)
    {
        var user = await _userRepository.GetUserByLoginAsync(login);
        
        if (user is null || !PasswordHasher.VerifyPassword(password, user.Password))
            throw new UnauthorizedAccessException("Неверный логин или пароль");

        var accessToken = GenerateAccessToken(user);
        var refreshToken = Guid.NewGuid().ToString();
        
        var refreshTokenEntity = new JwtRefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            Created = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(35),
            IsRevoked = false
        };
        
        await _jwtTokenRepository.AddRefreshTokenAsync(refreshTokenEntity);
        
        return (accessToken, refreshToken);
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var tokenEntity = await _jwtTokenRepository.GetRefreshTokenAsync(refreshToken);
        
        if (tokenEntity is null) throw new UnauthorizedAccessException("Invalid refresh token");
        
        await _jwtTokenRepository.RevokeRefreshTokenAsync(refreshToken);
        
        var newAccessToken = GenerateAccessToken(tokenEntity.User);
        var newRefreshToken = Guid.NewGuid().ToString();
        
        var newRefreshTokenEntity = new JwtRefreshToken
        {
            UserId = tokenEntity.User.Id,
            Token = newRefreshToken,
            Created = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(35),
            IsRevoked = false
        };
        
        await _jwtTokenRepository.AddRefreshTokenAsync(newRefreshTokenEntity);
        
        return (newAccessToken, newRefreshToken);
    }
    
    private string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role.ToString())
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

    public async Task LogoutAsync(string refreshToken)
    {
        await _jwtTokenRepository.RevokeRefreshTokenAsync(refreshToken);
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