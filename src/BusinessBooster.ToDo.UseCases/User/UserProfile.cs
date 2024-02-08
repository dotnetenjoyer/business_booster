using AutoMapper;
using BusinessBooster.ToDo.UseCases.User.CreateUser;

namespace BusinessBooster.ToDo.UseCases.User;

/// <summary>
/// User entities profile.
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public UserProfile()
    {
        CreateMap<CreateUserCommand, Domain.Entities.User>();
    }
}