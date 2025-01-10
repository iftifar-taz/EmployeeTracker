using Identity.Core.Domain.Entities;
using Identity.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Persistence
{
    public class UnitOfWork(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IUnitOfWork
    {
        private readonly DataContext _context = context;
        private bool _disposed;

        public UserManager<User> UserManager { get; } = userManager;
        public RoleManager<IdentityRole> RoleManager { get; } = roleManager;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
