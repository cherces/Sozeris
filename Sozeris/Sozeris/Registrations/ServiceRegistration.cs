using Sozeris.Services;
using Sozeris.Services.Interfaces;

namespace Sozeris.Registrations;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthService, AuthService>();
    }
}