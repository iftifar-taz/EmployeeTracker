using Identity.Application.DTOs;
using Identity.Application.Exceptions;
using Identity.Core.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.GetUsers
{
    public class GetUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserQuery, UserResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<UserResponseDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(query.UserId) ?? throw new NotFoundException("User does not exist.");
            return new UserResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
            };
        }
    }
}
