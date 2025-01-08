using EmployeeTracker.Context.Contracts;
using EmployeeTracker.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTracker.CQRS.Designations.DeleteDesignation
{
    public class DeleteDesignationCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteDesignationCommandHandler> logger) : IRequestHandler<DeleteDesignationCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<DeleteDesignationCommandHandler> _logger = logger;

        public async Task Handle(DeleteDesignationCommand command, CancellationToken cancellationToken)
        {
            var designation = await _unitOfWork.DesignationManager.FirstOrDefaultAsync(x => x.DesignationId == command.DesignationId, cancellationToken) ?? throw new BadRequestException("Designation does not exist.");
            _unitOfWork.DesignationManager.Remove(designation);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Designation deleted.");
        }
    }
}