namespace EmployeeTracker.CQRS.Designations.Common
{
    public class DesignationResponseDto
    {
        public Guid DesignationId { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int EmployeeCount { get; set; }
    }
}
