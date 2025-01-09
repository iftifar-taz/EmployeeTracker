using EmployeeTracker.Application.Exceptions;
using EmployeeTracker.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTracker.Application.Features.Departments.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteDepartmentCommandHandler> logger) : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<DeleteDepartmentCommandHandler> _logger = logger;

        public async Task Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.DepartmentManager.FirstOrDefaultAsync(x => x.DepartmentId == command.DepartmentId, cancellationToken) ?? throw new BadRequestException("Department does not exist.");
            _unitOfWork.DepartmentManager.Remove(department);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Department deleted.");
        }
    }
}
