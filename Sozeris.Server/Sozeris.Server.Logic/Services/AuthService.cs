using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sozeris.Server.Logic.Interfaces.Services;
using Sozeris.Server.Domain.Commons;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Domain.Models;
using Sozeris.Server.Logic.Common;
using DomainError = Sozeris.Server.Logic.Common.DomainError;

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

    public async Task<Result<AuthModel>> LoginAsync(string login, string password, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByLoginAsync(login, ct);
        
        if (user is null || !PasswordHasher.VerifyPassword(password, user.Password))
            return Result<AuthModel>.Fail(DomainError.Unauthorized("Invalid login or password"));

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
        
        await _jwtTokenRepository.AddRefreshTokenAsync(refreshTokenEntity, ct);
        var auth = new AuthModel() { AccessToken = accessToken, RefreshToken = refreshToken };
        
        return Result<AuthModel>.Ok(auth);
    }

    public async Task<Result<AuthModel>> RefreshTokenAsync(string refreshToken, CancellationToken ct)
    {
        var tokenEntity = await _jwtTokenRepository.GetRefreshTokenAsync(refreshToken, ct);
        
        if (tokenEntity is null) return Result<AuthModel>.Fail(DomainError.Unauthorized("Invalid refresh token"));
        
        await _jwtTokenRepository.RevokeRefreshTokenAsync(refreshToken, ct);
        
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
        
        await _jwtTokenRepository.AddRefreshTokenAsync(newRefreshTokenEntity, ct);
        var auth = new AuthModel() { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        
        return Result<AuthModel>.Ok(auth);
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

    public async Task<Result> LogoutAsync(string refreshToken, CancellationToken ct)
    {
        await _jwtTokenRepository.RevokeRefreshTokenAsync(refreshToken, ct);

        return Result.Ok();
    }
}