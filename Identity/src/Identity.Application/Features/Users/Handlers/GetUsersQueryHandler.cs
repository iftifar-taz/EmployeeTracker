using Identity.Application.Dtos;
using Identity.Application.Features.Users.Queries;
using Identity.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Handlers
{
    public class GetUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUsersQuery, IEnumerable<UserResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<UserResponseDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserManager.Users.AsNoTracking().Select(x => new UserResponseDto
            {
                UserId = x.Id,
                Email = x.Email,
            }).ToListAsync(cancellationToken);
        }
    }
}
