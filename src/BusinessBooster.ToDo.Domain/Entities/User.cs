using Microsoft.AspNetCore.Identity;

namespace BusinessBooster.ToDo.Domain.Entities;

/// <summary>
/// An application user.
/// </summary>
public class User : IdentityUser<long>
{
}