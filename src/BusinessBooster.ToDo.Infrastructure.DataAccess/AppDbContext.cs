using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.Infrastructure.DataAccess;

/// <summary>
/// Application database context.
/// </summary>
public class AppDbContext : IdentityDbContext<User, Role, long>, IDbContext
{
    /// <inheritdoc />
    public DbSet<Plan> Plans { get; set; }
    
    /// <inheritdoc />
    public DbSet<PlannedTask> Tasks { get; set; }

    /// <inheritdoc />
    public DbSet<TaskStatusRecord> TaskStatusHistory { get; set; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="Microsoft.EntityFrameworkCore.DbContext" />.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}