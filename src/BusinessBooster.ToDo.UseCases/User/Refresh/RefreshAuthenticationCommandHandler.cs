using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MediatR;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using BusinessBooster.ToDo.UseCases.User.Authenticate;
using AppUser = BusinessBooster.ToDo.Domain.Entities.User;

namespace BusinessBooster.ToDo.UseCases.User.Refresh;

/// <summary>
/// Handler for <see cref="RefreshAuthenticationCommand"/>.
/// </summary>
public class RefreshAuthenticationCommandHandler : IRequestHandler<RefreshAuthenticationCommand, Tokens>
{
    private readonly UserManager<AppUser> userManager;
    private readonly ITokenService tokenService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public RefreshAuthenticationCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<Tokens> Handle(RefreshAuthenticationCommand command, CancellationToken cancellationToken)
    {
        var claims = tokenService.GetTokenClaims(command.RefreshToken);
        
        var emailClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
        if (emailClaim == null)
        {
            throw new DomainException("The specified refresh token is invalid");
        }
        
        var user = await userManager.FindByEmailAsync(emailClaim.Value);
        if (user == null)
        {
            throw new DomainException("The user with the specified e-mail address was not found");
        }

        var tokens = GenerateTokens(user);
        return tokens;
    }
    
    private Tokens GenerateTokens(AppUser user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Email, user.Email)
        };

        var accessToken = tokenService.GenerateToken(claims, AuthenticationConstants.AccessTokenExpirationTime);
        var refreshToken = tokenService.GenerateToken(claims, AuthenticationConstants.RefreshTokenExpirationTime);

        return new Tokens(accessToken, refreshToken);
    }
}