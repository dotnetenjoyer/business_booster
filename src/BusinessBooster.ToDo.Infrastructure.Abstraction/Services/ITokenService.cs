using System.Security.Claims;
using BusinessBooster.ToDo.Domain.Entities;

namespace BusinessBooster.ToDo.Infrastructure.Abstraction.Services;

/// <summary>
/// Token service.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates JWT token based on claims.
    /// </summary>
    /// <param name="claims">Claims.</param>
    /// <param name="duration">Token duration.</param>
    /// <returns></returns>
    string GenerateToken(IEnumerable<Claim> claims, TimeSpan duration);

    /// <summary>
    /// Parse JWT token.
    /// </summary>
    /// <param name="token">JWT token.</param>
    /// <returns>Set of claims.</returns>
    IEnumerable<Claim> GetTokenClaims(string token);
}