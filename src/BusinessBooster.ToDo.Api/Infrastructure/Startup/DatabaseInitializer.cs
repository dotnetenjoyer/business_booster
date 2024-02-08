using Microsoft.EntityFrameworkCore;
using Extensions.Hosting.AsyncInitialization;
using BusinessBooster.ToDo.Infrastructure.DataAccess;

namespace BusinessBooster.ToDo.Api.Infrastructure.Startup;

/// <summary>
/// Contains database migration helper methods.
/// </summary>
public class DatabaseInitializer : IAsyncInitializer
{
    private readonly AppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DatabaseInitializer(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}