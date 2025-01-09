using EmployeeTracker.Context.Contracts;
using EmployeeTracker.CQRS.Employees.Common;
using EmployeeTracker.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.CQRS.Employees.GetEmployee
{
    public class GetEmployeeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEmployeeQuery, EmployeeResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<EmployeeResponseDto> Handle(GetEmployeeQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.EmployeeManager.AsNoTracking().Include(x => x.Department).Include(x => x.Designation).Select(x => new EmployeeResponseDto
            {
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                DateOfBirth = x.DateOfBirth,
                DateOfJoining = x.DateOfJoining,
                DateOfResignation = x.DateOfResignation,
                Address = x.Address,
                City = x.City,
                State = x.State,
                Country = x.Country,
                PostalCode = x.PostalCode,
                IsActive = x.IsActive,
                Department = new EmployeeDepartmentResponseDto
                {
                    DepartmentId = x.Department.DepartmentId,
                    DepartmentName = x.Department.DepartmentName,
                    DepartmentKey = x.Department.DepartmentKey,
                    Description = x.Department.Description,
                },
                Designation = new EmployeeDesignationResponseDto
                {
                    DesignationId = x.Designation.DesignationId,
                    DesignationName = x.Designation.DesignationName,
                    DesignationKey = x.Designation.DesignationKey,
                    Description = x.Designation.Description,
                },
            }).FirstOrDefaultAsync(x => x.EmployeeId == query.EmployeeId, cancellationToken)
            ?? throw new NotFoundException("Employee does not exist.");
        }
    }
}
