using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Core.Domain.Entities
{
    public class Designation
    {
        [Key]
        public Guid DesignationId { get; set; } = Guid.NewGuid();
        [MaxLength(64)]
        public required string DesignationName { get; set; }
        [MaxLength(16)]
        public required string DesignationKey { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
