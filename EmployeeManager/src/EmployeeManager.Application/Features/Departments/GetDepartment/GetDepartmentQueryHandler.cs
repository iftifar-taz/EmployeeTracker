using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Exceptions;
using EmployeeManager.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.Features.Departments.GetDepartment
{
    public class GetDepartmentQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDepartmentQuery, DepartmentResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<DepartmentResponseDto> Handle(GetDepartmentQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.DepartmentManager.AsNoTracking().Include(x => x.Employees).Select(x => new DepartmentResponseDto
            {
                DepartmentId = x.DepartmentId,
                DepartmentName = x.DepartmentName,
                DepartmentKey = x.DepartmentKey,
                Description = x.Description,
                EmployeeCount = x.Employees != null ? x.Employees.Count() : 0,
            }).FirstOrDefaultAsync(x => x.DepartmentId == query.DepartmentId, cancellationToken)
            ?? throw new NotFoundException("Department does not exist.");
        }
    }
}
