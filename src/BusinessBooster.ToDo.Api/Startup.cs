using BusinessBooster.ToDo.Api.Infrastructure.DependencyInjection;
using BusinessBooster.ToDo.Api.Infrastructure.Jwt;
using BusinessBooster.ToDo.Api.Infrastructure.Middlewares;
using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.Api;

public class Startup
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Configure application services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        
        services.AddScoped<IDbContext, AppDbContext>();
        services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();
        
        var jwtSetup = new JwtBearerOptionsSetup(configuration["JWT:Issuer"], configuration["JWT:Secret"]);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtSetup.Setup);

        var allowedHosts = configuration["AllowedHosts"].Split(", ");
        services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
            {
                policy
                    .WithOrigins(allowedHosts)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        InfrastructureModule.Register(services, configuration);
        ApplicationModule.Register(services, configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseCors("Default");

        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseAuthentication();    
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.Map("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
            
            endpoints.MapControllers();
        });
    }
}