using Identity.Application.Exceptions;
using Identity.Application.Features.Users.Commands;
using Identity.Core.Domain.Entities;
using Identity.Core.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.Handlers
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(command.Email);

            if (user != null)
            {
                throw new BadRequestException("User already exist.");
            }

            var newUser = new User
            {
                Email = command.Email,
                UserName = command.Email
            };

            var result = await _unitOfWork.UserManager.CreateAsync(newUser, command.PasswordRaw);

            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.FirstOrDefault()?.Description ?? string.Empty);
            }

            if (command.Roles is null)
            {
                await _unitOfWork.UserManager.AddToRoleAsync(newUser, "User");
            }
            else
            {
                await _unitOfWork.UserManager.AddToRolesAsync(newUser, command.Roles);
            }
        }
    }
}
