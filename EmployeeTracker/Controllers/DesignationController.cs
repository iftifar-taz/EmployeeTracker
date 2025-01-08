using Asp.Versioning;
using EmployeeTracker.CQRS.Designations.Common;
using EmployeeTracker.CQRS.Designations.CreateDesignation;
using EmployeeTracker.CQRS.Designations.DeleteDesignation;
using EmployeeTracker.CQRS.Designations.GetDesignation;
using EmployeeTracker.CQRS.Designations.GetDesignations;
using EmployeeTracker.CQRS.Designations.UpdateDesignation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTracker.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/designations")]
    public class DesignationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("", Name = "GetDesignations")]
        public async Task<ActionResult<IEnumerable<DesignationResponseDto>>> GetDesignations()
        {
            var response = await _mediator.Send(new GetDesignationsQuery());
            return Ok(response);
        }

        [HttpGet("{designationId}", Name = "GetDesignation")]
        public async Task<ActionResult<DesignationResponseDto>> GetDesignation([FromRoute] Guid designationId)
        {
            var response = await _mediator.Send(new GetDesignationQuery(designationId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("", Name = "CreateDesignation")]
        public async Task<IActionResult> CreateDesignation([FromBody] CreateDesignationCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{designationId}", Name = "UpdateDesignation")]
        public async Task<IActionResult> UpdateDesignation([FromRoute] Guid designationId, [FromBody] UpdateDesignationCommand command)
        {
            command.DesignationId = designationId;
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{designationId}", Name = "DeleteDesignation")]
        public async Task<IActionResult> DeleteDesignation([FromRoute] Guid designationId)
        {
            await _mediator.Send(new DeleteDesignationCommand(designationId));
            return Created();
        }
    }
}
