using Asp.Versioning;
using EmployeeTracker.CQRS.Sessions.CreateSession;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTracker.Controllers
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
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
