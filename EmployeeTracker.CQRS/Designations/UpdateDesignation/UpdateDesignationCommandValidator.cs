using FluentValidation;

namespace EmployeeTracker.CQRS.Designations.UpdateDesignation
{
    public class UpdateDesignationCommandValidator : AbstractValidator<UpdateDesignationCommand>
    {
        public UpdateDesignationCommandValidator()
        {
            RuleFor(x => x.DesignationName)
                .NotEmpty()
                .WithMessage("Designation Name is required.")
                .MaximumLength(64)
                .WithMessage("Designation Name must not exceed 64 characters.");
        }
    }
}
