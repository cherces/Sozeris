using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories.Interfaces;

public interface IJwtTokenRepository
{
    Task<bool> AddRefreshTokenAsync(int userId ,string refreshToken);
    Task<bool> DeleteRefreshTokenByUserIdAsync(int userId);
    Task<bool> DeleteRefreshTokenAsync(string refreshToken);
    Task<string?> GetRefreshTokenAsync(int userId);
    Task<int?> GetUserIdByRefreshTokenAsync(string refreshToken);
}