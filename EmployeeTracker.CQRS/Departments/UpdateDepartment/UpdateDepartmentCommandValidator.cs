using FluentValidation;

namespace EmployeeTracker.CQRS.Departments.UpdateDepartment
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty()
                .WithMessage("Department Name is required.")
                .MaximumLength(64)
                .WithMessage("Department Name must not exceed 64 characters.");
        }
    }
}
