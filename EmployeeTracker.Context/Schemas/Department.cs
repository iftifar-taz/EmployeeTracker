using System.ComponentModel.DataAnnotations;

namespace EmployeeTracker.Context.Schemas
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; } = Guid.NewGuid();
        [MaxLength(64)]
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
