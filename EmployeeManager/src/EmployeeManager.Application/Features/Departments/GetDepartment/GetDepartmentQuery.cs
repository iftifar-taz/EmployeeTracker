using EmployeeManager.Application.DTOs;
using MediatR;

namespace EmployeeManager.Application.Features.Departments.GetDepartment
{
    public class GetDepartmentQuery(Guid departmentId) : IRequest<DepartmentResponseDto>
    {
        public Guid DepartmentId { get; private set; } = departmentId;
    }
}
