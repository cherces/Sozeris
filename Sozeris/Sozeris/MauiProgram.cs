using Microsoft.Extensions.Logging;
using Sozeris.Config;
using Sozeris.Registrations;
using System.Net.Http;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Sozeris.Services;
using Sozeris.ViewModels;

namespace Sozeris;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddSingleton<ApiConfig>(new ApiConfig("https://api.com"));

        builder.Services.AddHttpClient<AuthService>();

        builder.Services.AddServices();
        builder.Services.AddPages();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}