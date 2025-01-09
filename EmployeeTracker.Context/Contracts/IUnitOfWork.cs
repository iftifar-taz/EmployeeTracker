using EmployeeTracker.Context.Schemas;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.Context.Contracts
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
