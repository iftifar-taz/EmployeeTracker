using MediatR;

namespace EmployeeTracker.Application.Features.Departments.DeleteDepartment
{
    public class DeleteDepartmentCommand(Guid departmentId) : IRequest
    {
        public Guid DepartmentId { get; private set; } = departmentId;
    }
}
