using System.ComponentModel.DataAnnotations;
using BusinessBooster.ToDo.UseCases.User.Authenticate;
using MediatR;

namespace BusinessBooster.ToDo.UseCases.User.Refresh;

/// <summary>
/// Refresh user authentication.
/// </summary>
public class RefreshAuthenticationCommand : IRequest<Tokens>
{
    /// <summary>
    /// Refresh token.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string RefreshToken { get; set; }
}