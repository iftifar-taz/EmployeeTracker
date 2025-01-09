using Asp.Versioning;
using EmployeeTracker.CQRS.Employees.Common;
using EmployeeTracker.CQRS.Employees.CreateEmployee;
using EmployeeTracker.CQRS.Employees.DeleteEmployee;
using EmployeeTracker.CQRS.Employees.GetEmployee;
using EmployeeTracker.CQRS.Employees.GetEmployees;
using EmployeeTracker.CQRS.Employees.UpdateEmployee;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTracker.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/employees")]
    public class EmployeeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("", Name = "GetEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetEmployees()
        {
            var response = await _mediator.Send(new GetEmployeesQuery());
            return Ok(response);
        }

        [HttpGet("{employeeId}", Name = "GetEmployee")]
        public async Task<ActionResult<EmployeeResponseDto>> GetEmployee([FromRoute] Guid employeeId)
        {
            var response = await _mediator.Send(new GetEmployeeQuery(employeeId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("", Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{employeeId}", Name = "UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid employeeId, [FromBody] UpdateEmployeeCommand command)
        {
            command.EmployeeId = employeeId;
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{employeeId}", Name = "DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeId)
        {
            await _mediator.Send(new DeleteEmployeeCommand(employeeId));
            return Ok();
        }
    }
}
