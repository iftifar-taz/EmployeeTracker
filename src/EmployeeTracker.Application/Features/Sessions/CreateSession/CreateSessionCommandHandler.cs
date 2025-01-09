using EmployeeTracker.Application.DTOs;
using EmployeeTracker.Application.Exceptions;
using EmployeeTracker.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeTracker.Application.Features.Sessions.CreateSession
{
    public class CreateSessionCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, ILogger<CreateSessionCommandHandler> logger) : IRequestHandler<CreateSessionCommand, SessionResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly ILogger<CreateSessionCommandHandler> _logger = logger;

        public async Task<SessionResponseDto> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(command.Email);
            if (user is null)
            {
                _logger.LogWarning($"Invalid Credentials for {command.Email}");
                throw new BadRequestException("Invalid Credentials");
            }
            var result = await _unitOfWork.UserManager.CheckPasswordAsync(user, command.PasswordRaw);
            if (!result)
            {
                _logger.LogWarning($"Invalid Credentials for {command.Email}");
                throw new BadRequestException("Invalid Credentials");
            }

            var token = await _jwtTokenService.GenerateToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            var refrshTokenValidityIn = _jwtTokenService.GetRefrshTokenValidityIn();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refrshTokenValidityIn);
            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogWarning($"{command.Email} created token.");
            return new SessionResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
            };
        }
    }
}
