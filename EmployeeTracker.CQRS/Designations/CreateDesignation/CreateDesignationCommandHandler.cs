using EmployeeTracker.Context.Contracts;
using EmployeeTracker.Context.Schemas;
using EmployeeTracker.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTracker.CQRS.Designations.CreateDesignation
{
    public class CreateDesignationCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateDesignationCommandHandler> logger) : IRequestHandler<CreateDesignationCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateDesignationCommandHandler> _logger = logger;

        public async Task Handle(CreateDesignationCommand command, CancellationToken cancellationToken)
        {
            var designation = await _unitOfWork.DesignationManager.FirstOrDefaultAsync(x => string.Equals(x.DesignationName, command.DesignationName, StringComparison.OrdinalIgnoreCase), cancellationToken);
            if (designation != null)
            {
                throw new BadRequestException("Designation already exists.");
            }
            var newDesignation = new Designation
            {
                DesignationName = command.DesignationName,
                Description = command.Description,
            };

            await _unitOfWork.DesignationManager.AddAsync(newDesignation, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Designation creaded.");
        }
    }
}