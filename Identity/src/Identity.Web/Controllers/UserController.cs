using Asp.Versioning;
using Identity.Application.Dtos;
using Identity.Application.Features.Users.Commands;
using Identity.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<UserController> _logger = logger;

        [Authorize(Roles = "Admin")]
        [HttpGet("", Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            _logger.LogInformation("GetUsers called");
            var response = await _mediator.Send(new GetUsersQuery());
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUser([FromRoute] string userId)
        {
            _logger.LogInformation("GetUser called");
            var response = await _mediator.Send(new GetUserQuery(userId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("", Name = "CreateUser")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> CreateUser([FromBody] CreateUserCommand command)
        {
            _logger.LogInformation("CreateUser called");
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> DeleteUser([FromRoute] string userId)
        {
            _logger.LogInformation("DeleteUser called");
            await _mediator.Send(new DeleteUserCommand(userId));
            return Ok();
        }
    }
}
