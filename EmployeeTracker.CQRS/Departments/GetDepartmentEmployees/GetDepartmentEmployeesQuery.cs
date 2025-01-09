using MediatR;

namespace EmployeeTracker.CQRS.Departments.GetDepartmentEmployees
{
    public class GetDepartmentEmployeesQuery(Guid departmentId) : IRequest<IEnumerable<DepartmentEmployeeResponseDto>>
    {
        public Guid DepartmentId { get; private set; } = departmentId;
    }
}
