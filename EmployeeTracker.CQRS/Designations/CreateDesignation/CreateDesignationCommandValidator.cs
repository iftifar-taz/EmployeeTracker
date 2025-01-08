using FluentValidation;

namespace EmployeeTracker.CQRS.Designations.CreateDesignation
{
    public class CreateDesignationCommandValidator : AbstractValidator<CreateDesignationCommand>
    {
        public CreateDesignationCommandValidator()
        {
            RuleFor(x => x.DesignationName)
                .NotEmpty()
                .WithMessage("Designation Name is required.")
                .MaximumLength(64)
                .WithMessage("Designation Name must not exceed 64 characters.");
        }
    }
}
