namespace EmployeeManager.Application.DTOs
{
    public class EmployeeDesignationResponseDto
    {
        public Guid DesignationId { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public string DesignationKey { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
