using Identity.Application.Exceptions;
using Identity.Core.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.DeleteUser
{
    public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(command.UserId) ?? throw new NotFoundException("User does not exist.");
            await _unitOfWork.UserManager.DeleteAsync(user);
        }
    }
}
