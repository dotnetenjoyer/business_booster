using System.Security.Claims;
using MediatR;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using Microsoft.AspNetCore.Identity;
using AppUser = BusinessBooster.ToDo.Domain.Entities.User;

namespace BusinessBooster.ToDo.UseCases.User.Authenticate;

/// <summary>
/// Handler for <see cref="AuthenticateUserCommand"/>.
/// </summary>
internal class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Tokens>
{
    private readonly SignInManager<AppUser> signInManager;
    private readonly ITokenService tokenService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AuthenticateUserCommandHandler(SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        this.signInManager = signInManager;
        this.tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<Tokens> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await signInManager.UserManager.FindByEmailAsync(command.Email);
        if (user == null)
        {
            throw new DomainException("The combination of email and password is incorrect.");
        }
        
        var signInResult = await signInManager.CheckPasswordSignInAsync(user, command.Password, true);
        ValidateSignInResult(signInResult, command.Email);
        
        var tokens = GenerateTokens(user);
        return tokens;
    }
    

    private void ValidateSignInResult(SignInResult signInResult, string userEmail)
    {
        if (signInResult.Succeeded)
        {
            return;
        }
        
        if (signInResult.IsNotAllowed)
        {
            throw new DomainException($"User {userEmail} is not allowed to sign in.");
        }

        if (signInResult.IsLockedOut)
        {
            throw new DomainException($"User {userEmail} is locked out.");
        }

        throw new DomainException("The combination of email and password is incorrect.");
    }

    private Tokens GenerateTokens(AppUser user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Email, user.Email)
        };

        var accessToken = tokenService.GenerateToken(claims, AuthenticationConstants.AccessTokenExpirationTime);
        var refreshToken = tokenService.GenerateToken(claims, AuthenticationConstants.RefreshTokenExpirationTime);

        return new Tokens(accessToken, refreshToken);
    }
}