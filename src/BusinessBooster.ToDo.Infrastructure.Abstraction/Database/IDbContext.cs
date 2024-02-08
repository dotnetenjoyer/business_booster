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
    /// Collection of application tasks.
    /// </summary>
    // DbSet<Task> Tasks { get; set; }
    
    /// <summary>
    /// Collection of application to do lists.
    /// </summary>
    // DbSet<ToDoList> ToDoLists { get; set; }
}