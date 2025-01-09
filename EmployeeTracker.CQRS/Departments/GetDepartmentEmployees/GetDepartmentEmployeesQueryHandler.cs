using EmployeeTracker.Context.Contracts;
using EmployeeTracker.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.CQRS.Departments.GetDepartmentEmployees
{
    public class GetDepartmentEmployeesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDepartmentEmployeesQuery, IEnumerable<DepartmentEmployeeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<DepartmentEmployeeResponseDto>> Handle(GetDepartmentEmployeesQuery query, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.DepartmentManager.AsNoTracking().Include(x => x.Employees).FirstOrDefaultAsync(x => x.DepartmentId == query.DepartmentId, cancellationToken) ?? throw new NotFoundException("Department does not exist.");
            if (department.Employees == null)
            {
                return [];
            }

            return department.Employees.Select(x => new DepartmentEmployeeResponseDto
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
            });
        }
    }
}
