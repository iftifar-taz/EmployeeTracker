using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Employees.GetEmployees
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeResponseDto>>
    {
    }
}
