using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Designations.GetDesignation
{
    public class GetDesignationQuery(Guid designationId) : IRequest<DesignationResponseDto>
    {
        public Guid DesignationId { get; private set; } = designationId;
    }
}
