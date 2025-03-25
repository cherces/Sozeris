using Sozeris.Pages;
using Sozeris.ViewModels;

namespace Sozeris.Registrations;

public static class PageRegistration
{
    public static void AddPages(this IServiceCollection services)
    {
        services.AddTransient<LoginPage>(); 
        services.AddTransient<MainPage>();
    }
}