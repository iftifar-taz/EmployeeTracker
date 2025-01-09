using EmployeeTracker.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public DbSet<Employee> EmployeeManager { get; }
        public DbSet<Designation> DesignationManager { get; }
        public DbSet<Department> DepartmentManager { get; }
        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        Task<int> SaveChangesAsync();
    }
}
