using Sozeris.Models;
using Sozeris.Models.Enums;

namespace Sozeris.Logic.Services.Interfaces;

public interface IUserSessionService
{
    Task<bool> IsAuthenticatedAsync();
    Task<UserRole?> GetRoleAsync();
    Task SaveTokensAsync(JwtTokenModel token);
    Task ClearSessionAsync(); 
}