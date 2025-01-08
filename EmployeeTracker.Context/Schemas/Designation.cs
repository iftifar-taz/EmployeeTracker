using System.ComponentModel.DataAnnotations;

namespace EmployeeTracker.Context.Schemas
{
    public class Designation
    {
        [Key]
        public Guid DesignationId { get; set; } = Guid.NewGuid();
        [MaxLength(64)]
        public required string DesignationName { get; set; }
        public string? Description { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
