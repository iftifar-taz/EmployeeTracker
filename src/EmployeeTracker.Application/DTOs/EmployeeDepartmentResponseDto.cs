namespace EmployeeTracker.Application.DTOs
{
    public class EmployeeDepartmentResponseDto
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentKey { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
