using Microsoft.AspNetCore.Identity;

namespace EmployeeTracker.Context.Schemas
{
    public class User : IdentityUser
    {
        public Employee? Employee { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
