using System.Security.Claims;
using Identity.Core.Domain.Entities;

namespace Identity.Core.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(User user);
        string GenerateRefreshToken();
        int GetRefrshTokenValidityIn();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
