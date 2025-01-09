using EmployeeTracker.Context.Schemas;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.Context
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().HasIndex(u => u.LastName);
            builder.Entity<Department>().HasIndex(u => u.DepartmentName).IsUnique();
            builder.Entity<Designation>().HasIndex(u => u.DesignationName).IsUnique();

            builder.Entity<Employee>().HasOne(x => x.Designation).WithMany(x => x.Employees).HasForeignKey(x => x.DesignationId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Employee>().HasOne(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
