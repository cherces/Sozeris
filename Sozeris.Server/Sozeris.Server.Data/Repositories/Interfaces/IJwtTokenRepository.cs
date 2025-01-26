using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories.Interfaces;

public interface IJwtTokenRepository
{
    Task<bool> AddRefreshTokenAsync(int userId ,string refreshToken);
    Task<bool> DeleteRefreshTokenAsync(int userId);
    Task<string?> GetRefreshTokenAsync(int userId);
    Task<int?> GetUserIdByRefreshTokenAsync(string refreshToken);
}