using System.Security.Claims;
using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Services;

public interface IAuthService
{ 
    string GenerateAccessTokenAsync(User user);
    Task<string> GenerateRefreshTokenAsync(int userId);
    ClaimsPrincipal? ValidateAccessTokenAsync(string token);
}