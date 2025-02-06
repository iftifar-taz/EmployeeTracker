using Identity.Application.Dtos;
using MediatR;

namespace Identity.Application.Features.Users.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<UserResponseDto>>
    {
    }
}
