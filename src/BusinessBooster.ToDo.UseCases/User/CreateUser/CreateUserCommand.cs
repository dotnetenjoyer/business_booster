using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BusinessBooster.ToDo.UseCases.User.CreateUser;

/// <summary>
/// Creates a new user.
/// </summary>
public class CreateUserCommand : IRequest<IdDto<long>>
{
    /// <summary>
    /// User email.
    /// </summary>
    [Required]
    [EmailAddress]
    [DisplayName("Email")]
    public string Email { get; init; }
    
    /// <summary>
    /// User name.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [DisplayName("User name")]
    public string UserName { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    public string Password { get; init; }

    /// <summary>
    /// Confirm password.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [DisplayName("Confirm password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; init; }
}