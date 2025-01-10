using EmployeeManager.Application.DTOs;
using MediatR;

namespace EmployeeManager.Application.Features.Employees.GetEmployees
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeResponseDto>>
    {
    }
}
