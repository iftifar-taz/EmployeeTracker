using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EmployeeTracker.Context.Contracts;
using EmployeeTracker.Context.Schemas;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeTracker.CQRS.Sessions.CreateSession
{
    public class CreateSessionCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateSessionCommandHandler> logger, IConfiguration configuration) : IRequestHandler<CreateSessionCommand, SessionResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateSessionCommandHandler> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public async Task<SessionResponseDto> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(command.Email);
            if (user is null)
            {
                _logger.LogWarning($"Invalid Credentials for {command.Email}");
                throw new Exception("Invalid Credentials");
            }    
            var result = await _unitOfWork.UserManager.CheckPasswordAsync(user, command.PasswordRaw);
            if (!result)
            {
                _logger.LogWarning($"Invalid Credentials for {command.Email}");
                throw new Exception("Invalid Credentials");
            }

            var token = await GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration.GetSection("JWTSetting").GetSection("RefrshTokenValidityIn").Value!, out int refrshTokenValidityIn);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refrshTokenValidityIn);
            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return new SessionResponseDto
            {
                Token = token,
            };
        }

        private async Task<string> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSetting").GetSection("SecurityKey").Value!);
            var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
            List<Claim> claims = [
                new (JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new (JwtRegisteredClaimNames.NameId, user.Id ?? string.Empty),
                new (JwtRegisteredClaimNames.Aud, _configuration.GetSection("JWTSetting").GetSection("ValidAudience").Value!),
                new (JwtRegisteredClaimNames.Iss, _configuration.GetSection("JWTSetting").GetSection("ValidIssuer").Value!),
            ];

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
