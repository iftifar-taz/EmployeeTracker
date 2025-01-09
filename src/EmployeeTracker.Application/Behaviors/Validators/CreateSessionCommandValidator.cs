using EmployeeTracker.Application.Features.Sessions.CreateSession;
using FluentValidation;

namespace EmployeeTracker.Application.Behaviors.Validators
{
    public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address format.");

            RuleFor(x => x.PasswordRaw)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}
