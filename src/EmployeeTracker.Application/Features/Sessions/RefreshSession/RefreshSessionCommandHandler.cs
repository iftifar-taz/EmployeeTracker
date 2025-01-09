using EmployeeTracker.Application.DTOs;
using EmployeeTracker.Application.Exceptions;
using EmployeeTracker.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeTracker.Application.Features.Sessions.RefreshSession
{
    public class RefreshSessionCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, ILogger<RefreshSessionCommandHandler> logger) : IRequestHandler<RefreshSessionCommand, SessionResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly ILogger<RefreshSessionCommandHandler> _logger = logger;

        public async Task<SessionResponseDto> Handle(RefreshSessionCommand command, CancellationToken cancellationToken)
        {
            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(command.Token);
            var user = await _unitOfWork.UserManager.FindByEmailAsync(command.Email);
            if (principal is null || user is null || user.RefreshToken != command.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new BadRequestException("Invalid credentials!");
            }

            var newJwtToken = await _jwtTokenService.GenerateToken(user);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            var refrshTokenValidityIn = _jwtTokenService.GetRefrshTokenValidityIn();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refrshTokenValidityIn);
            await _unitOfWork.UserManager.UpdateAsync(user);
            _logger.LogWarning($"{command.Email} created token with refresh token.");
            return new SessionResponseDto
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken,
            };
        }
    }
}
