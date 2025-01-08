using MediatR;

namespace EmployeeTracker.CQRS.Sessions.CreateSession
{
    public class CreateSessionCommand : IRequest<SessionResponseDto>
    {
        public required string Email { get; set; }
        public required string PasswordRaw { get; set; }
    }
}
