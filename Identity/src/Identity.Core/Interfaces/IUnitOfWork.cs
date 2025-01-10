using Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        Task<int> SaveChangesAsync();
    }
}
