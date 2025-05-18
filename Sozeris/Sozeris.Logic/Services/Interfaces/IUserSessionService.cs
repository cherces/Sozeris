using Sozeris.Models;

namespace Sozeris.Logic.Services.Interfaces;

public interface IUserSessionService
{
    Task<bool> IsAuthenticatedAsync();
    Task<string> GetRoleAsync();
    Task SaveTokensAsync(JwtTokenModel token);
    Task ClearSessionAsync(); 
}