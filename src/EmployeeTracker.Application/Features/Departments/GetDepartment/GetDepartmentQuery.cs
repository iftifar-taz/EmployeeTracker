using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Departments.GetDepartment
{
    public class GetDepartmentQuery(Guid departmentId) : IRequest<DepartmentResponseDto>
    {
        public Guid DepartmentId { get; private set; } = departmentId;
    }
}
