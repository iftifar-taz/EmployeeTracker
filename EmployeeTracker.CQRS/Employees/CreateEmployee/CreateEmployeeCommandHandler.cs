using EmployeeTracker.Context.Contracts;
using EmployeeTracker.Context.Schemas;
using EmployeeTracker.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTracker.CQRS.Employees.CreateEmployee
{
    public class CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateEmployeeCommandHandler> logger) : IRequestHandler<CreateEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger = logger;

        public async Task Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.DepartmentManager.FirstOrDefaultAsync(x => x.DepartmentId == command.DepartmentId, cancellationToken) ?? throw new BadRequestException("Department does not exist.");
            var designation = await _unitOfWork.DesignationManager.FirstOrDefaultAsync(x => x.DesignationId == command.DesignationId, cancellationToken) ?? throw new BadRequestException("Designation does not exist.");

            var newEmployee = new Employee
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                DateOfBirth = command.DateOfBirth,
                DateOfJoining = command.DateOfJoining,
                Address = command.Address,
                City = command.City,
                State = command.State,
                Country = command.Country,
                PostalCode = command.PostalCode,
                IsActive = true,
                Department = department,
                DepartmentId = department.DepartmentId,
                Designation = designation,
                DesignationId = designation.DesignationId,
            };

            await _unitOfWork.EmployeeManager.AddAsync(newEmployee, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Employee creaded.");
        }
    }
}
