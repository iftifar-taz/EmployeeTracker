using MediatR;

namespace EmployeeTracker.CQRS.Designations.DeleteDesignation
{
    public class DeleteDesignationCommand(Guid designationId) : IRequest
    {
        public Guid DesignationId { get; private set; } = designationId;
    }
}
