using Identity.Application.Exceptions;
using Identity.Core.Domain.Entities;
using Identity.Core.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.CreateUser
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _ = await _unitOfWork.UserManager.FindByEmailAsync(command.Email) ?? throw new NotFoundException("User already exist.");

            var nweUser = new User
            {
                Email = command.Email,
                UserName = command.Email
            };

            var result = await _unitOfWork.UserManager.CreateAsync(nweUser, command.PasswordRaw);

            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.FirstOrDefault()?.Description ?? string.Empty);
            }

            if (command.Roles is null)
            {
                await _unitOfWork.UserManager.AddToRoleAsync(nweUser, "User");
            }
            else
            {
                await _unitOfWork.UserManager.AddToRolesAsync(nweUser, command.Roles);
            }
        }
    }
}
