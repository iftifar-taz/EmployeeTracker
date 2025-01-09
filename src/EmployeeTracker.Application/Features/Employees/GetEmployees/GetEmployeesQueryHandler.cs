using EmployeeTracker.Application.DTOs;
using EmployeeTracker.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.Application.Features.Employees.GetEmployees
{
    public class GetEmployeesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<EmployeeResponseDto>> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
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
            }).ToListAsync(cancellationToken);
        }
    }
}
