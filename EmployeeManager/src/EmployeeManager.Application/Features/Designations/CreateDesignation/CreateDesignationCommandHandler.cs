using EmployeeManager.Application.Exceptions;
using EmployeeManager.Core.Domain.Entities;
using EmployeeManager.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Application.Features.Designations.CreateDesignation
{
    public class CreateDesignationCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateDesignationCommandHandler> logger) : IRequestHandler<CreateDesignationCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateDesignationCommandHandler> _logger = logger;

        public async Task Handle(CreateDesignationCommand command, CancellationToken cancellationToken)
        {
            var designation = await _unitOfWork.DesignationManager.FirstOrDefaultAsync(x => x.DesignationKey == command.DesignationKey, cancellationToken);
            if (designation != null)
            {
                throw new BadRequestException("Designation already exists.");
            }
            var newDesignation = new Designation
            {
                DesignationName = command.DesignationName,
                DesignationKey = command.DesignationKey,
                Description = command.Description,
            };

            await _unitOfWork.DesignationManager.AddAsync(newDesignation, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Designation creaded.");
        }
    }
}
