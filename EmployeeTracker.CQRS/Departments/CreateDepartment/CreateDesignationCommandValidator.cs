using FluentValidation;

namespace EmployeeTracker.CQRS.Departments.CreateDepartment
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty()
                .WithMessage("Department Name is required.")
                .MaximumLength(64)
                .WithMessage("Department Name must not exceed 64 characters.");

            RuleFor(x => x.DepartmentKey)
                .NotEmpty()
                .WithMessage("Department Key is required.")
                .MaximumLength(16)
                .WithMessage("Department Key must not exceed 16 characters.");
        }
    }
}
