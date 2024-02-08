namespace BusinessBooster.ToDo.Infrastructure.Abstraction.Services;

/// <summary>
/// Logged user accessor routine.
/// </summary>
public interface ILoggedUserAccessor
{
    /// <summary>
    /// Get current logged user identifier.
    /// </summary>
    /// <returns>Current logged user identifier.</returns>
    int GetCurrentUserId();
    
    /// <summary>
    /// Return <c>True</c> if user is authenticated. 
    /// </summary>
    bool IsAuthenticated();
}