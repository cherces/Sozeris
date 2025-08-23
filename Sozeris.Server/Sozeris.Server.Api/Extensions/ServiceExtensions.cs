using Sozeris.Server.Logic.Interfaces.Services;
using Sozeris.Server.Logic.Services;

namespace Sozeris.Server.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IDeliveryService, DeliveryService>();
    }
}