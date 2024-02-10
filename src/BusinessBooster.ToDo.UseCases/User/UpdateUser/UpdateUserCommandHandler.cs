using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessBooster.ToDo.UseCases.User.UpdateUser;

/// <summary>
/// Handler for <see cref="UpdateUserCommand"/>.
/// </summary>
internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly UserManager<Domain.Entities.User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdateUserCommandHandler(ILoggedUserAccessor loggedUserAccessor, UserManager<Domain.Entities.User> userManager)
    {
        this.loggedUserAccessor = loggedUserAccessor;
        this.userManager = userManager;
    }

    /// <inheritdoc />
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(loggedUserAccessor.GetCurrentUserId().ToString());
        user.Email = request.Email;
        user.UserName = request.UserName;
        await userManager.UpdateAsync(user);
    }
}