using MediatR;

namespace EmployeeManager.Application.Features.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommand(Guid employeeId) : IRequest
    {
        public Guid EmployeeId { get; private set; } = employeeId;
    }
}
