using EmployeeTracker.Context.Contracts;
using EmployeeTracker.CQRS.Departments.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.CQRS.Departments.GetDepartments
{
    public class GetDepartmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDepartmentsQuery, IEnumerable<DepartmentResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<DepartmentResponseDto>> Handle(GetDepartmentsQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.DepartmentManager.AsNoTracking().Include(x => x.Employees).Select(x => new DepartmentResponseDto
            {
                DepartmentName = x.DepartmentName,
                Description = x.Description,
                EmployeeCount = x.Employees != null ? x.Employees.Count() : 0,
            }).ToListAsync(cancellationToken);
        }
    }
}
