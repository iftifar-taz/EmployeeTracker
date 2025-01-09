using MediatR;

namespace EmployeeTracker.CQRS.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommand(Guid employeeId) : IRequest
    {
        public Guid EmployeeId { get; private set; } = employeeId;
    }
}
