using Sozeris.Server.Logic.Services;
using Sozeris.Server.Logic.Services.Interfaces;

namespace Sozeris.Server.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}