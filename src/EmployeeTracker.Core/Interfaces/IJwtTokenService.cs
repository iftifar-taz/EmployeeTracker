using System.Security.Claims;
using EmployeeTracker.Core.Domain.Entities;

namespace EmployeeTracker.Core.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(User user);
        string GenerateRefreshToken();
        int GetRefrshTokenValidityIn();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
