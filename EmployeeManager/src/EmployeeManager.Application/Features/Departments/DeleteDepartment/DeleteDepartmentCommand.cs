using MediatR;

namespace EmployeeManager.Application.Features.Departments.DeleteDepartment
{
    public class DeleteDepartmentCommand(Guid departmentId) : IRequest
    {
        public Guid DepartmentId { get; private set; } = departmentId;
    }
}
