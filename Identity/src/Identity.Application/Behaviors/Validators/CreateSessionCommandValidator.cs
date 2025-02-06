using FluentValidation;
using Identity.Application.Features.Sessions.Commands;

namespace Identity.Application.Behaviors.Validators
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
