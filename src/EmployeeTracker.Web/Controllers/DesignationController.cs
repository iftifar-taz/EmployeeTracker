using Asp.Versioning;
using EmployeeTracker.Application.DTOs;
using EmployeeTracker.Application.Features.Designations.CreateDesignation;
using EmployeeTracker.Application.Features.Designations.DeleteDesignation;
using EmployeeTracker.Application.Features.Designations.GetDesignation;
using EmployeeTracker.Application.Features.Designations.GetDesignationEmployees;
using EmployeeTracker.Application.Features.Designations.GetDesignations;
using EmployeeTracker.Application.Features.Designations.UpdateDesignation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTracker.Web.Controllers
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

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost("", Name = "CreateDesignation")]
        public async Task<IActionResult> CreateDesignation([FromBody] CreateDesignationCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut("{designationId}", Name = "UpdateDesignation")]
        public async Task<IActionResult> UpdateDesignation([FromRoute] Guid designationId, [FromBody] UpdateDesignationCommand command)
        {
            command.DesignationId = designationId;
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("{designationId}", Name = "DeleteDesignation")]
        public async Task<IActionResult> DeleteDesignation([FromRoute] Guid designationId)
        {
            await _mediator.Send(new DeleteDesignationCommand(designationId));
            return Ok();
        }

        [HttpGet("{designationId}/employees", Name = "GetDesignationEmployees")]
        public async Task<ActionResult<IEnumerable<DesignationEmployeeResponseDto>>> GetDesignationEmployees([FromRoute] Guid designationId)
        {
            var response = await _mediator.Send(new GetDesignationEmployeesQuery(designationId));
            return Ok(response);
        }
    }
}
