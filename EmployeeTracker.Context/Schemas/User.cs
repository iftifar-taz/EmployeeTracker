using Microsoft.AspNetCore.Identity;

namespace EmployeeTracker.Context.Schemas
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
