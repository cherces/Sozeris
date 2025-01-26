using System.Security.Claims;
using Sozeris.Server.Models.Commons;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services.Interfaces;

public interface IAuthService
{ 
    string GenerateAccessTokenAsync(User user);
    Task<string> GenerateRefreshTokenAsync(int userId);
    ClaimsPrincipal? ValidateAccessTokenAsync(string token);
}