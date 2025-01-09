using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Designations.GetDesignations
{
    public class GetDesignationsQuery() : IRequest<IEnumerable<DesignationResponseDto>>
    {
    }
}
