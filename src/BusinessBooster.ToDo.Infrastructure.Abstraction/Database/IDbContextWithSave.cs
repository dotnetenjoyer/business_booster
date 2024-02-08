namespace BusinessBooster.ToDo.Infrastructure.Abstraction.Database;

/// <summary>
/// Data base context with save support.
/// </summary>
public interface IDbContextWithSave
{
    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}