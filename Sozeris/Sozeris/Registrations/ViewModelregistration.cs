using Sozeris.ViewModels;

namespace Sozeris.Registrations;

public static class ViewModelRegistration
{
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<LoginViewModel>();
        services.AddTransient<ProductsCatalogViewModel>();
        services.AddSingleton<CartViewModel>();
        services.AddTransient<UserProfileViewModel>();
        services.AddTransient<DeliveryHistoryViewModel>();
    }
}