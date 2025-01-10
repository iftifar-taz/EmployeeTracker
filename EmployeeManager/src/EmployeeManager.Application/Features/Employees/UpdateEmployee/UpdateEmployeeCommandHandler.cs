using EmployeeManager.Application.Exceptions;
using EmployeeManager.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManager.Application.Features.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateEmployeeCommandHandler> logger) : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger = logger;

        public async Task Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeManager.FirstOrDefaultAsync(x => x.EmployeeId == command.EmployeeId, cancellationToken) ?? throw new BadRequestException("Employee does not exist.");
            var department = await _unitOfWork.DepartmentManager.FirstOrDefaultAsync(x => x.DepartmentId == command.DepartmentId, cancellationToken) ?? throw new BadRequestException("Department does not exist.");
            var designation = await _unitOfWork.DesignationManager.FirstOrDefaultAsync(x => x.DesignationId == command.DesignationId, cancellationToken) ?? throw new BadRequestException("Designation does not exist.");

            employee.FirstName = command.FirstName;
            employee.LastName = command.LastName;
            employee.Email = command.Email;
            employee.PhoneNumber = command.PhoneNumber;
            employee.DateOfBirth = command.DateOfBirth;
            employee.DateOfJoining = command.DateOfJoining;
            employee.DateOfResignation = command.DateOfResignation;
            employee.Address = command.Address;
            employee.City = command.City;
            employee.State = command.State;
            employee.Country = command.Country;
            employee.PostalCode = command.PostalCode;
            employee.IsActive = true;
            employee.Department = department;
            employee.DepartmentId = department.DepartmentId;
            employee.Designation = designation;
            employee.DesignationId = designation.DesignationId;

            _unitOfWork.EmployeeManager.Update(employee);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Employee updated.");
        }
    }
}
