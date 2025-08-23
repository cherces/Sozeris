using Sozeris.Server.Data.Repositories;
using Sozeris.Server.Domain.Interfaces.Repositories;

namespace Sozeris.Server.Api.Extensions;

public static class RepositoryExtensions
{
    public static void AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IJwtTokenRepository, JwtTokenRepository>();
        services.AddScoped<IDeliveryHistoryRepository, DeliveryHistoryRepository>();
    }
}