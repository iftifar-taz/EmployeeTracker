using Microsoft.AspNetCore.Identity;

namespace EmployeeTracker.Core.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
