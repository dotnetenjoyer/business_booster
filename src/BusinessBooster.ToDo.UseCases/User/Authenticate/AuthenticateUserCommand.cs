using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BusinessBooster.ToDo.UseCases.User.Authenticate;

/// <summary>
/// Command to authenticate user.
/// </summary>
public class AuthenticateUserCommand : IRequest<Tokens>
{
    /// <summary>
    /// User email.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Email { get; set; }

    /// <summary>
    /// User password.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}