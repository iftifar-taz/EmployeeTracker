using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Features.Users.GetUsers
{
    public class GetUserQuery(string userId) : IRequest<UserResponseDto>
    {
        public string UserId { get; private set; } = userId;
    }
}
