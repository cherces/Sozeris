using Sozeris.Config;
using Newtonsoft.Json;
using System.Text;
using Sozeris.Models;
using Sozeris.Services.Interfaces;

namespace Sozeris.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ApiConfig _apiConfig;
    
    private const string IsAuthenticatedKey = "IsAuthenticated";
    
    public AuthService(HttpClient httpClient, ApiConfig apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig;
    }
    public bool IsUserAuthenticated
    {
        get => Preferences.Get(IsAuthenticatedKey, false);
        private set => Preferences.Set(IsAuthenticatedKey, value);
    }
    public async Task<bool> LoginAsync(LoginModel loginModel)
    {
        var json = JsonConvert.SerializeObject(loginModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_apiConfig.BaseUrl}/login", content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        var tokens = JsonConvert.DeserializeObject<JwtTokenModel>(result);

        try
        {
            await SecureStorage.SetAsync("AccessToken", tokens.AccessToken);
            await SecureStorage.SetAsync("RefreshToken", tokens.RefreshToken);
        }
        catch (Exception ex)
        {
            return false;
        }
        
        IsUserAuthenticated = true;
        
        return true;
    }
    
    public void Logout()
    {
        IsUserAuthenticated = false;
    }
}