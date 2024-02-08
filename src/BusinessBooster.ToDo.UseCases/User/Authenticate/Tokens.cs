namespace BusinessBooster.ToDo.UseCases.User.Authenticate;

/// <summary>
/// Authentication tokens.
/// </summary>
/// <param name="AccessToken">Access tokens.</param>
/// <param name="RefreshToken">Refresh tokens.</param>
public record Tokens(string AccessToken, string RefreshToken);