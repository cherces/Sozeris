using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models;

namespace Sozeris.Logic.Services;

public class UserSessionService : IUserSessionService
{
    private const string accessTokenKey = "AccessToken";
    private const string refreshTokenKey = "RefreshToken";
    private const string roleKey = "UserRole";

    public async Task SaveTokensAsync(JwtTokenModel tokens)
    {
        await SecureStorage.SetAsync(accessTokenKey, tokens.AccessToken);
        await SecureStorage.SetAsync(refreshTokenKey, tokens.RefreshToken);
        await SecureStorage.SetAsync(roleKey, "User");
    }

    public Task ClearSessionAsync()
    {
        SecureStorage.Remove(accessTokenKey);
        SecureStorage.Remove(refreshTokenKey);
        SecureStorage.Remove(roleKey);
        
        return Task.CompletedTask;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        string? token = await SecureStorage.GetAsync(accessTokenKey);
        return !string.IsNullOrEmpty(token);
    }

    public async Task<string> GetRoleAsync()
    {
        return await SecureStorage.GetAsync(roleKey) ?? string.Empty;
    }
}