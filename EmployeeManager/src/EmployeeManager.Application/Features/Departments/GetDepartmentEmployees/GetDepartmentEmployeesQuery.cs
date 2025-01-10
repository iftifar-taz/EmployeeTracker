using EmployeeManager.Application.DTOs;
using MediatR;

namespace EmployeeManager.Application.Features.Departments.GetDepartmentEmployees
{
    public class GetDepartmentEmployeesQuery(Guid departmentId) : IRequest<IEnumerable<DepartmentEmployeeResponseDto>>
    {
        public Guid DepartmentId { get; private set; } = departmentId;
    }
}
