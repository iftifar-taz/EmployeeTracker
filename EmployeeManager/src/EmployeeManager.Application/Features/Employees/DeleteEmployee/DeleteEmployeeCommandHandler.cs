using EmployeeManager.Application.Exceptions;
using EmployeeManager.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Application.Features.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteEmployeeCommandHandler> logger) : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger = logger;

        public async Task Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeManager.FirstOrDefaultAsync(x => x.EmployeeId == command.EmployeeId, cancellationToken) ?? throw new BadRequestException("Employee does not exist.");
            _unitOfWork.EmployeeManager.Remove(employee);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Employee deleted.");
        }
    }
}
