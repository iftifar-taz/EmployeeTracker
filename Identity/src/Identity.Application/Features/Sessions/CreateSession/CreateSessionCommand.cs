using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Features.Sessions.CreateSession
{
    public class CreateSessionCommand : IRequest<SessionResponseDto>
    {
        public required string Email { get; set; }
        public required string PasswordRaw { get; set; }
    }
}
