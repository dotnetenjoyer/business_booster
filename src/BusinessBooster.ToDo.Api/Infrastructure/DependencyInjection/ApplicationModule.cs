using BusinessBooster.ToDo.Api.Infrastructure.Jwt;
using BusinessBooster.ToDo.Api.Infrastructure.Services;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using BusinessBooster.ToDo.UseCases.Plans;

namespace BusinessBooster.ToDo.Api.Infrastructure.DependencyInjection;

/// <summary>
/// Module that register application dependencies.
/// </summary>
public class ApplicationModule 
{
    /// <summary>
    /// Register application dependencies.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Application configurations.</param>
    internal static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITokenService, JWTTokenService>();
        services.AddScoped<ILoggedUserAccessor, LoggedUserAccessor>();

        services.AddScoped<PlanService>();
    }
}