using Asp.Versioning;
using EmployeeTracker.Application.DTOs;
using EmployeeTracker.Application.Features.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTracker.Web.Controllers
{
    [Authorize]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    public class UserV2Controller(IMediator mediator, ILogger<UserV2Controller> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<UserV2Controller> _logger = logger;

        [Authorize(Roles = "Admin")]
        [HttpGet("", Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            _logger.LogInformation("GetUsers called");
            var response = await _mediator.Send(new GetUsersQuery());
            return Ok(response);
        }
    }
}
