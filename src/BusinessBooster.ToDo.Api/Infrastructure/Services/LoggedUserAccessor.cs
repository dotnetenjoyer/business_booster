using System.Security.Claims;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;

namespace BusinessBooster.ToDo.Api.Infrastructure.Services;

/// <summary>
/// Logged user accessor.
/// </summary>
public class LoggedUserAccessor : ILoggedUserAccessor
{
    private readonly IHttpContextAccessor httpContextAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public LoggedUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public int GetCurrentUserId()
    {
        if (TryGetCurrentUserId(out var userId))
        {
            return userId;
        }

        throw new InvalidOperationException("There is no logged user.");
    }

    /// <inheritdoc />
    public bool IsAuthenticated()
    {
        return TryGetCurrentUserId(out var userId);
    }
    
    private  bool TryGetCurrentUserId(out int userId)
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("There is no active HTTP context.");
        }
        
        var currentUserId = httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (!string.IsNullOrEmpty(currentUserId))
        {
            userId = int.Parse(currentUserId);
            return true;
        }

        userId = -1;
        return false;
    } 
}