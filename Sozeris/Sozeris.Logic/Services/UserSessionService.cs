using Sozeris.Logic.Helpers;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models;
using Sozeris.Models.Enums;

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
        var role = JwtHelper.ExtractUserRole(tokens.AccessToken);

        if (role != null)
        {
            await SecureStorage.SetAsync(roleKey, ((int)role).ToString());
        }    }

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

    public async Task<UserRole?> GetRoleAsync()
    {
        var roleStr = await SecureStorage.GetAsync(roleKey);
        if (int.TryParse(roleStr, out int roleValue) && Enum.IsDefined(typeof(UserRole), roleValue))
        {
            return (UserRole)roleValue;
        }

        return null;
    }
}