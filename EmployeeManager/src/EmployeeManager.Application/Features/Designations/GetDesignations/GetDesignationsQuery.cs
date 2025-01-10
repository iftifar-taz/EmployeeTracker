using EmployeeManager.Application.DTOs;
using MediatR;

namespace EmployeeManager.Application.Features.Designations.GetDesignations
{
    public class GetDesignationsQuery() : IRequest<IEnumerable<DesignationResponseDto>>
    {
    }
}
