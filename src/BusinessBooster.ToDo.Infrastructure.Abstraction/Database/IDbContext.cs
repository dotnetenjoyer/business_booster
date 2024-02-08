using BusinessBooster.ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.Infrastructure.Abstraction.Database;

/// <summary>
/// Define interface for application database context.
/// </summary>
public interface IDbContext : IDbContextWithSave
{
    /// <summary>
    /// Collection of application users.
    /// </summary>
    DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Collection of plans.
    /// </summary>
    DbSet<Plan> Plans { get; set; }
    
    /// <summary>
    /// Collection of planned tasks.
    /// </summary>
    DbSet<PlannedTask> Tasks { get; set; }

    /// <summary>
    /// History of task statuses.
    /// </summary>
    DbSet<TaskStatusRecord> TaskStatusHistory { get; set; }
}