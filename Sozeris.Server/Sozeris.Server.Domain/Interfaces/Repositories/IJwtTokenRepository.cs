using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IJwtTokenRepository
{
    Task AddRefreshTokenAsync(JwtRefreshToken refreshToken);
    Task RevokeRefreshTokenAsync(string refreshToken);
    Task<JwtRefreshToken?> GetRefreshTokenAsync(string refreshToken);
}