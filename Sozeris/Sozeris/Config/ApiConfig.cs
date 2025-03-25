namespace Sozeris.Config;

public class ApiConfig
{
    public string BaseUrl { get; set; }

    public ApiConfig(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
}