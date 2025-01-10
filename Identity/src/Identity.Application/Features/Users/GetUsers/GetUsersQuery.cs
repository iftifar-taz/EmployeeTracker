using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Features.Users.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserResponseDto>>
    {
    }
}
