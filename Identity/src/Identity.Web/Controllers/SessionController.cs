using Asp.Versioning;
using Identity.Application.Dtos;
using Identity.Application.Features.Sessions.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/sessions")]
    public class SessionController(IMediator mediator, ILogger<SessionController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<SessionController> _logger = logger;

        [HttpPost("", Name = "CreateSession")]
        public async Task<ActionResult<SessionResponseDto>> CreateSession([FromBody] CreateSessionCommand command)
        {
            _logger.LogInformation("CreateSession called");
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("refresh-session", Name = "RefreshSession")]
        public async Task<ActionResult<SessionResponseDto>> RefreshSession([FromBody] RefreshSessionCommand command)
        {
            _logger.LogInformation("RefreshSession called");
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
