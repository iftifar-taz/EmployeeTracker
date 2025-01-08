using EmployeeTracker.CQRS.Designations.Common;
using MediatR;

namespace EmployeeTracker.CQRS.Designations.GetDesignations
{
    public class GetDesignationsQuery() : IRequest<IEnumerable<DesignationResponseDto>>
    {
    }
}
