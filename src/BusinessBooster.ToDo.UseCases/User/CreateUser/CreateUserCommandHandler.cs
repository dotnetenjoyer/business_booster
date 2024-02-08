using AutoMapper;
using BusinessBooster.ToDo.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessBooster.ToDo.UseCases.User.CreateUser;

/// <summary>
/// Handler for <see cref="CreateUserCommand"/>.
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdDto<long>>
{
    private readonly UserManager<Domain.Entities.User> userManager;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateUserCommandHandler(UserManager<Domain.Entities.User> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IdDto<long>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = mapper.Map<Domain.Entities.User>(command);
        var result = await userManager.CreateAsync(user, command.Password);
        ValidateResult(result);
        return user.Id;
    }

    private void ValidateResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            var errors = new ValidationErrors();
            foreach (var error in result.Errors)
            {
                errors.AddError(error.Code, error.Description);
            }

            throw new ValidationException(errors);
        }
    }
}