using MediatR;

namespace EmployeeTracker.CQRS.Departments.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest
    {
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
    }
}
