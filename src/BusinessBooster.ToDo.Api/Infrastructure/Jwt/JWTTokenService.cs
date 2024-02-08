using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BusinessBooster.ToDo.Api.Infrastructure.Jwt;

/// <summary>
/// JWT token service.
/// </summary>
public class JWTTokenService : ITokenService
{
    private readonly TokenValidationParameters tokenValidationParameters;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public JWTTokenService(IOptionsMonitor<JwtBearerOptions> jwtOptionsMonitor)
    {
        tokenValidationParameters = jwtOptionsMonitor.Get(JwtBearerDefaults.AuthenticationScheme)
            .TokenValidationParameters;
    }
    
    /// <inhertidoc />
    public string GenerateToken(IEnumerable<Claim> claims, TimeSpan duration)
    {
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.Add(duration),
            issuer: tokenValidationParameters.ValidIssuer,
            audience: tokenValidationParameters.ValidAudience,
            signingCredentials: new SigningCredentials(tokenValidationParameters.IssuerSigningKey,
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <inhertidoc />
    public IEnumerable<Claim> GetTokenClaims(string token)
    {
        var claimsPrincipal = new JwtSecurityTokenHandler()
            .ValidateToken(token, tokenValidationParameters, out var jwtToken);

        return claimsPrincipal.Claims;
    }
}