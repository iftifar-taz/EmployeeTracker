using FluentValidation;
using Identity.Application.Features.Sessions.RefreshSession;

namespace Identity.Application.Behaviors.Validators
{
    public class RefreshSessionCommandValidator : AbstractValidator<RefreshSessionCommand>
    {
        public RefreshSessionCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address format.");

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token is required.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh Token is required.");
        }
    }
}
