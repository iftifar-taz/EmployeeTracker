using EmployeeTracker.Context.Contracts;
using EmployeeTracker.CQRS.Designations.Common;
using EmployeeTracker.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.CQRS.Designations.GetDesignation
{
    public class GetDesignationQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDesignationQuery, DesignationResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<DesignationResponseDto> Handle(GetDesignationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.DesignationManager.AsNoTracking().Include(x => x.Employees).Select(x => new DesignationResponseDto
            {
                DesignationName = x.DesignationName,
                Description = x.Description,
                EmployeeCount = x.Employees != null ? x.Employees.Count() : 0,
            }).FirstOrDefaultAsync(x => x.DesignationId == query.DesignationId, cancellationToken)
            ?? throw new NotFoundException("Designation does not exist.");
        }
    }
}
