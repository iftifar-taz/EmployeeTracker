using EmployeeTracker.Application.Features.Designations.UpdateDesignation;
using FluentValidation;

namespace EmployeeTracker.Application.Behaviors.Validators
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

            RuleFor(x => x.DesignationKey)
                .NotEmpty()
                .WithMessage("Designation Key is required.")
                .MaximumLength(16)
                .WithMessage("Designation Key must not exceed 16 characters.");
        }
    }
}
