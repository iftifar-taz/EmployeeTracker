using MediatR;

namespace EmployeeTracker.CQRS.Departments.DeleteDepartment
{
    public class DeleteDepartmentCommand(Guid departmentId) : IRequest
    {
        public Guid DepartmentId { get; private set; } = departmentId;
    }
}
