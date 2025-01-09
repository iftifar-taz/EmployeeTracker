using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Designations.GetDesignationEmployees
{
    public class GetDesignationEmployeesQuery(Guid designationId) : IRequest<IEnumerable<DesignationEmployeeResponseDto>>
    {
        public Guid DesignationId { get; private set; } = designationId;
    }
}
