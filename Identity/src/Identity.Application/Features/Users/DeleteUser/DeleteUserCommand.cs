using MediatR;

namespace Identity.Application.Features.Users.DeleteUser
{
    public class DeleteUserCommand(string userId) : IRequest
    {
        public string UserId { get; private set; } = userId;
    }
}
