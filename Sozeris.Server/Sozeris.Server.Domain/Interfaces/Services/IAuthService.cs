using System.Security.Claims;
using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<(string AccessToken, string RefreshToken)> LoginAsync(string login, string password);
    Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(string refreshToken);
    ClaimsPrincipal? ValidateAccessTokenAsync(string token);
}