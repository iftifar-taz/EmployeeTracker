using MediatR;

namespace EmployeeTracker.CQRS.Users.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserResponseDto>>
    {
    }
}
