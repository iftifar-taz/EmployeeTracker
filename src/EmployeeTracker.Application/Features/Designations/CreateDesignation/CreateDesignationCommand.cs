using MediatR;

namespace EmployeeTracker.Application.Features.Designations.CreateDesignation
{
    public class CreateDesignationCommand : IRequest
    {
        public required string DesignationName { get; set; }
        public required string DesignationKey { get; set; }
        public string? Description { get; set; }
    }
}
