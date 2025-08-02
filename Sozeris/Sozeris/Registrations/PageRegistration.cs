using Sozeris.Pages;
using Sozeris.ViewModels;

namespace Sozeris.Registrations;

public static class PageRegistration
{
    public static void AddPages(this IServiceCollection services)
    {
        services.AddTransient<LoginPage>(); 
        services.AddTransient<HomePage>();
        services.AddTransient<LoadingPage>();
        services.AddTransient<UserProfilePage>();
        services.AddTransient<UserRegistrationPage>();
        services.AddTransient<UserSubscriptionPage>();
        services.AddTransient<ProductsCatalogPage>();
        services.AddTransient<CartPage>();
        services.AddTransient<DeliveryHistoryPage>();
    }
}