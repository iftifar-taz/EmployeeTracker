using MediatR;

namespace EmployeeManager.Application.Features.Designations.DeleteDesignation
{
    public class DeleteDesignationCommand(Guid designationId) : IRequest
    {
        public Guid DesignationId { get; private set; } = designationId;
    }
}
