using EmployeeTracker.Context.Contracts;
using EmployeeTracker.Context.Schemas;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.Context
{
    public class UnitOfWork(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IUnitOfWork
    {
        private readonly DataContext _context = context;
        private bool _disposed;

        public UserManager<User> UserManager { get; } = userManager;
        public RoleManager<IdentityRole> RoleManager { get; } = roleManager;
        public DbSet<Employee> Employeemanager { get; } = context.Employees;
        public DbSet<Designation> DesignationManager { get; } = context.Designations;
        public DbSet<Department> DepartmentManager { get; } = context.Departments;

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
