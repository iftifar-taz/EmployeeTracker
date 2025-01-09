using MediatR;

namespace EmployeeTracker.Application.Features.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommand(Guid employeeId) : IRequest
    {
        public Guid EmployeeId { get; private set; } = employeeId;
    }
}
