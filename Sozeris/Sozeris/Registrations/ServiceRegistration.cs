using Sozeris.Logic.Services;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Services;

namespace Sozeris.Registrations;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IUserSessionService, UserSessionService>();
    }
}