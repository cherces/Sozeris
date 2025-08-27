using System.Security.Claims;
using Sozeris.Server.Domain.Models;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Interfaces.Services;

public interface IAuthService
{
    Task<Result<AuthModel>> LoginAsync(string login, string password, CancellationToken ct);
    Task<Result<AuthModel>> RefreshTokenAsync(string refreshToken, CancellationToken ct);
    Task<Result> LogoutAsync(string refreshToken, CancellationToken ct);
}