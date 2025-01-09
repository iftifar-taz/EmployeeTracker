using MediatR;

namespace EmployeeTracker.Application.Features.Departments.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest
    {
        public required string DepartmentName { get; set; }
        public required string DepartmentKey { get; set; }
        public string? Description { get; set; }
    }
}
