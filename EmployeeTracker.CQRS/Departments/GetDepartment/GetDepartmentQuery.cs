using EmployeeTracker.CQRS.Departments.Common;
using MediatR;

namespace EmployeeTracker.CQRS.Departments.GetDepartment
{
    public class GetDepartmentQuery(Guid departmentId) : IRequest<DepartmentResponseDto>
    {
        public Guid DepartmentId { get; private set; } = departmentId;
    }
}
