using Asp.Versioning;
using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Features.Employees.CreateEmployee;
using EmployeeManager.Application.Features.Employees.DeleteEmployee;
using EmployeeManager.Application.Features.Employees.GetEmployee;
using EmployeeManager.Application.Features.Employees.GetEmployees;
using EmployeeManager.Application.Features.Employees.UpdateEmployee;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Web.Controllers
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

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost("", Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut("{employeeId}", Name = "UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid employeeId, [FromBody] UpdateEmployeeCommand command)
        {
            command.EmployeeId = employeeId;
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("{employeeId}", Name = "DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeId)
        {
            await _mediator.Send(new DeleteEmployeeCommand(employeeId));
            return Ok();
        }
    }
}
