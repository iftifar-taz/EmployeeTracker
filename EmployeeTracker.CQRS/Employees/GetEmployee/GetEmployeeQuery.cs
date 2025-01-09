using EmployeeTracker.CQRS.Employees.Common;
using MediatR;

namespace EmployeeTracker.CQRS.Employees.GetEmployee
{
    public class GetEmployeeQuery(Guid employeeId) : IRequest<EmployeeResponseDto>
    {
        public Guid EmployeeId { get; private set; } = employeeId;
    }
}
