using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IJwtTokenRepository
{
    Task AddRefreshTokenAsync(JwtRefreshToken refreshToken, CancellationToken ct);
    Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken ct);
    Task<JwtRefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken ct);
}