using EmployeeManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public DbSet<Employee> EmployeeManager { get; }
        public DbSet<Designation> DesignationManager { get; }
        public DbSet<Department> DepartmentManager { get; }

        Task<int> SaveChangesAsync();
    }
}
