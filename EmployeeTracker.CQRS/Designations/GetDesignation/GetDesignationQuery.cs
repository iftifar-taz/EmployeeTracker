using EmployeeTracker.CQRS.Designations.Common;
using MediatR;

namespace EmployeeTracker.CQRS.Designations.GetDesignation
{
    public class GetDesignationQuery(Guid designationId) : IRequest<DesignationResponseDto>
    {
        public Guid DesignationId { get; private set; } = designationId;
    }
}
