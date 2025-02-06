using FluentValidation;
using Identity.Application.Features.Users.Commands;

namespace Identity.Application.Behaviors.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
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
