using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BusinessBooster.ToDo.Api.Infrastructure.Jwt;

/// <summary>
/// Setup JWT bearer authentication.
/// </summary>
public class JwtBearerOptionsSetup
{
    private readonly string issuer;
    private readonly string secret;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="issuer">Jwt issuer.</param>
    /// <param name="secret">Jwt secret.</param>
    public JwtBearerOptionsSetup(string issuer, string secret)
    {
        this.issuer = issuer;
        this.secret = secret;
    }
    
    /// <summary>
    /// Setups jwt bearer options.
    /// </summary>
    /// <param name="options">Jwt bearer options.</param>
    public void Setup(JwtBearerOptions options)
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {    
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = false,
            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
        };
    }
}