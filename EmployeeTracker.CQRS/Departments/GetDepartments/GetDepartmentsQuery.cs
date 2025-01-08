using EmployeeTracker.CQRS.Departments.Common;
using MediatR;

namespace EmployeeTracker.CQRS.Departments.GetDepartments
{
    public class GetDepartmentsQuery() : IRequest<IEnumerable<DepartmentResponseDto>>
    {
    }
}
