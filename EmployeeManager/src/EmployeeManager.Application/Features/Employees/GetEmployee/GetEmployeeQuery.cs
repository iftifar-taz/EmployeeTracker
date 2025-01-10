using EmployeeManager.Application.DTOs;
using MediatR;

namespace EmployeeManager.Application.Features.Employees.GetEmployee
{
    public class GetEmployeeQuery(Guid employeeId) : IRequest<EmployeeResponseDto>
    {
        public Guid EmployeeId { get; private set; } = employeeId;
    }
}
