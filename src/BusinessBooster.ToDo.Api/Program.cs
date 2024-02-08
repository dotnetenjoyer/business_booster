using BusinessBooster.ToDo.Api;
using Serilog;

/// <summary>
/// Entry application point.
/// </summary>
internal sealed class Program
{
    /// <summary>
    /// Entry point method.
    /// </summary>
    /// <param name="args">Program arguments.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            var hostEnvironment = services.GetRequiredService<IWebHostEnvironment>();
            var logPath = Path.Combine(hostEnvironment.ContentRootPath, "Logs/log.txt");
        
            configuration
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day);
        });
        
        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();
        startup.Configure(app, app.Environment);

        await app.InitAsync();
        await app.RunAsync();
    }
}