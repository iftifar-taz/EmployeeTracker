﻿using EmployeeManager.Application.Exceptions;
using EmployeeManager.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Application.Features.Departments.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateDepartmentCommandHandler> logger) : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<UpdateDepartmentCommandHandler> _logger = logger;

        public async Task Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.DepartmentManager.FirstOrDefaultAsync(x => x.DepartmentId == command.DepartmentId, cancellationToken) ?? throw new BadRequestException("Department does not exist.");
            department.DepartmentName = command.DepartmentName;
            department.DepartmentKey = command.DepartmentKey;
            department.Description = command.Description;

            _unitOfWork.DepartmentManager.Update(department);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Department updated.");
        }
    }
}
