using EmployeeManager.Application.DTOs;
using EmployeeManager.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.Features.Designations.GetDesignations
{
    public class GetDesignationsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDesignationsQuery, IEnumerable<DesignationResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<DesignationResponseDto>> Handle(GetDesignationsQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.DesignationManager.AsNoTracking().Include(x => x.Employees).Select(x => new DesignationResponseDto
            {
                DesignationId = x.DesignationId,
                DesignationName = x.DesignationName,
                DesignationKey = x.DesignationKey,
                Description = x.Description,
                EmployeeCount = x.Employees != null ? x.Employees.Count() : 0,
            }).ToListAsync(cancellationToken);
        }
    }
}
