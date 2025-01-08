using EmployeeTracker.Context.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracker.CQRS.Users.GetUsers
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
