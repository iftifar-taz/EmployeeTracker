using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Employees.GetEmployee
{
    public class GetEmployeeQuery(Guid employeeId) : IRequest<EmployeeResponseDto>
    {
        public Guid EmployeeId { get; private set; } = employeeId;
    }
}
