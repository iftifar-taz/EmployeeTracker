using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Sessions.RefreshSession
{
    public class RefreshSessionCommand : IRequest<SessionResponseDto>
    {
        public required string Email { get; set; } = null!;
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
