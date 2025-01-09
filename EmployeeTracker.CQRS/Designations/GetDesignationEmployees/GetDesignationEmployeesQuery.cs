using MediatR;

namespace EmployeeTracker.CQRS.Designations.GetDesignationEmployees
{
    public class GetDesignationEmployeesQuery(Guid designationId) : IRequest<IEnumerable<DesignationEmployeeResponseDto>>
    {
        public Guid DesignationId { get; private set; } = designationId;
    }
}
