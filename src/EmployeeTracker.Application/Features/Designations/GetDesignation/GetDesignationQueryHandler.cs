using EmployeeTracker.Application.DTOs;
using EmployeeTracker.Application.Exceptions;
using EmployeeTracker.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.Application.Features.Designations.GetDesignation
{
    public class GetDesignationQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDesignationQuery, DesignationResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<DesignationResponseDto> Handle(GetDesignationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.DesignationManager.AsNoTracking().Include(x => x.Employees).Select(x => new DesignationResponseDto
            {
                DesignationId = x.DesignationId,
                DesignationName = x.DesignationName,
                DesignationKey = x.DesignationKey,
                Description = x.Description,
                EmployeeCount = x.Employees != null ? x.Employees.Count() : 0,
            }).FirstOrDefaultAsync(x => x.DesignationId == query.DesignationId, cancellationToken)
            ?? throw new NotFoundException("Designation does not exist.");
        }
    }
}
