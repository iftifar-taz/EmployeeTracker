using EmployeeManager.Application.DTOs;
using MediatR;

namespace EmployeeManager.Application.Features.Departments.GetDepartments
{
    public class GetDepartmentsQuery() : IRequest<IEnumerable<DepartmentResponseDto>>
    {
    }
}
