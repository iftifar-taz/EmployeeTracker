using MediatR;

namespace EmployeeManager.Application.Features.Designations.UpdateDesignation
{
    public class UpdateDesignationCommand : IRequest
    {
        public Guid DesignationId { get; set; }
        public required string DesignationName { get; set; }
        public required string DesignationKey { get; set; }
        public string? Description { get; set; }
    }
}
