using EmployeeManager.Application.DTOs;
using MediatR;

namespace EmployeeManager.Application.Features.Designations.GetDesignationEmployees
{
    public class GetDesignationEmployeesQuery(Guid designationId) : IRequest<IEnumerable<DesignationEmployeeResponseDto>>
    {
        public Guid DesignationId { get; private set; } = designationId;
    }
}
