using EmployeeManager.Core.Domain.Entities;
using EmployeeManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Infrastructure.Persistence
{
    public class UnitOfWork(DataContext context) : IUnitOfWork
    {
        private readonly DataContext _context = context;
        private bool _disposed;

        public DbSet<Employee> EmployeeManager { get; } = context.Employees;
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
