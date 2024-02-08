namespace BusinessBooster.ToDo.UseCases.User.Authenticate;

/// <summary>
/// Contains authentication constants.
/// </summary>
internal static class AuthenticationConstants
{
    /// <summary>
    /// Access token expiration time.
    /// </summary>
    public static readonly TimeSpan AccessTokenExpirationTime = TimeSpan.FromHours(3);
    
    /// <summary>
    /// Refresh token expiration time.
    /// </summary>
    public static readonly TimeSpan RefreshTokenExpirationTime = TimeSpan.FromDays(20);
}