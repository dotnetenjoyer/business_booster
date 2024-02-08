using BusinessBooster.ToDo.Api.Infrastructure.Startup;
using BusinessBooster.ToDo.UseCases.User;
using BusinessBooster.ToDo.UseCases.User.Authenticate;

namespace BusinessBooster.ToDo.Api.Infrastructure.DependencyInjection;

/// <summary>
/// Module that register infrastructure dependencies.
/// </summary>
public class InfrastructureModule
{
    /// <summary>
    /// Register infrastructure dependencies.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Application configurations.</param>
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(UserProfile));

        services.AddMediatR(configuration => configuration
            .RegisterServicesFromAssemblies(typeof(AuthenticateUserCommand).Assembly));

        services.AddAsyncInitializer<DatabaseInitializer>();
    }
}