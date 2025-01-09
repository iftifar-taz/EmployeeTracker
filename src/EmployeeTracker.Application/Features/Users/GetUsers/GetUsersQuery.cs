using EmployeeTracker.Application.DTOs;
using MediatR;

namespace EmployeeTracker.Application.Features.Users.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserResponseDto>>
    {
    }
}
