using EmployeeTracker.CQRS.Employees.Common;
using MediatR;

namespace EmployeeTracker.CQRS.Employees.GetEmployees
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeResponseDto>>
    {
    }
}
