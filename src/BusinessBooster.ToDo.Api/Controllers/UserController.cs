using Microsoft.AspNetCore.Mvc;
using MediatR;
using BusinessBooster.ToDo.UseCases;
using BusinessBooster.ToDo.UseCases.User.Authenticate;
using BusinessBooster.ToDo.UseCases.User.CreateUser;
using BusinessBooster.ToDo.UseCases.User.Refresh;

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
    /// Authenticates an user.
    /// </summary>
    [HttpPost]
    [Route("auth")]
    public Task<Tokens> AuthenticateAsync(AuthenticateUserCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);

    /// <summary>
    /// Register an user.
    /// </summary>
    [HttpPost]
    [Route("register")]
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