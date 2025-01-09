using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Departments.GetDepartments
{
    public class GetDepartmentsQuery() : IRequest<IEnumerable<DepartmentResponseDto>>
    {
    }
}
