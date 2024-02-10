using Microsoft.AspNetCore.Mvc;
using MediatR;
using BusinessBooster.ToDo.UseCases;
using BusinessBooster.ToDo.UseCases.User.Authenticate;
using BusinessBooster.ToDo.UseCases.User.CreateUser;
using BusinessBooster.ToDo.UseCases.User.Refresh;
using BusinessBooster.ToDo.UseCases.User.UpdateUser;

namespace BusinessBooster.ToDo.Api.Controllers;

/// <summary>
/// Contains API methods to manage application users.
/// </summary>
[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator"></param>
    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Updates user.
    /// </summary>
    [HttpPost]
    public Task UpdateAsync(UpdateUserCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken); 
    
    /// <summary>
    /// Authenticates an user.
    /// </summary>
    [HttpPost("auth")]
    public Task<Tokens> AuthenticateAsync(AuthenticateUserCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);

    /// <summary>
    /// Register an user.
    /// </summary>
    [HttpPost("register")]
    public Task<IdDto<long>> RegisterAsync(CreateUserCommand command, CancellationToken cancellationToken) 
        => mediator.Send(command, cancellationToken);

    /// <summary>
    /// Refresh the user authentication by refresh token.
    /// </summary>
    [HttpPost]
    [Route("refresh")]
    public Task<Tokens> RefreshAsync(RefreshAuthenticationCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);
}