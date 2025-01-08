using MediatR;

namespace EmployeeTracker.CQRS.Departments.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest
    {
        public Guid DepartmentId { get; set; }
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
    }
}
