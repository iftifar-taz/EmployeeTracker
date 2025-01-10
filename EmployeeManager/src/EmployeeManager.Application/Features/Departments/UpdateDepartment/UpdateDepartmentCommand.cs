using MediatR;

namespace EmployeeManager.Application.Features.Departments.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest
    {
        public Guid DepartmentId { get; set; }
        public required string DepartmentName { get; set; }
        public required string DepartmentKey { get; set; }
        public string? Description { get; set; }
    }
}
