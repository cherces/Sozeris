namespace Sozeris.Models.Config;

public class SozerisServerApiConfig
{
    public string BaseUrl { get; set; }

    public SozerisServerApiConfig(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
}