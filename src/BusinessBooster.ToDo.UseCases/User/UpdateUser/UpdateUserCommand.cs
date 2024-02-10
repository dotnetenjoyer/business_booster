using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BusinessBooster.ToDo.UseCases.User.UpdateUser;

/// <summary>
/// Command to update user.
/// </summary>
public class UpdateUserCommand : IRequest
{
    /// <summary>
    /// User name.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string UserName { get; set; }
    
    /// <summary>
    /// Email.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Email { get; set; }
}