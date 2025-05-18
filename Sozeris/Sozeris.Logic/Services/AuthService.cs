using Sozeris.Models.Config;
using Newtonsoft.Json;
using System.Text;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models;
using Sozeris.Models.Commons;

namespace Sozeris.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly SozerisServerApiConfig _apiConfig;
    
    public AuthService(HttpClient httpClient, SozerisServerApiConfig apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig;
    }
    
    public async Task<JwtTokenModel> LoginAsync(LoginModel loginModel)
    {
        var json = JsonConvert.SerializeObject(loginModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_apiConfig.BaseUrl}/login", content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        var tokens = JsonConvert.DeserializeObject<JwtTokenModel>(result);
        
        return tokens ?? new JwtTokenModel(); 
    }
}