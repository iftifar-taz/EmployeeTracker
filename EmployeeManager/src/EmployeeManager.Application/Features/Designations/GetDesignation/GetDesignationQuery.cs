using EmployeeManager.Application.DTOs;
using MediatR;

namespace EmployeeManager.Application.Features.Designations.GetDesignation
{
    public class GetDesignationQuery(Guid designationId) : IRequest<DesignationResponseDto>
    {
        public Guid DesignationId { get; private set; } = designationId;
    }
}
