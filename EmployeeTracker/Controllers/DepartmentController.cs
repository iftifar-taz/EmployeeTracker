using Asp.Versioning;
using EmployeeTracker.CQRS.Departments.Common;
using EmployeeTracker.CQRS.Departments.CreateDepartment;
using EmployeeTracker.CQRS.Departments.DeleteDepartment;
using EmployeeTracker.CQRS.Departments.GetDepartment;
using EmployeeTracker.CQRS.Departments.GetDepartments;
using EmployeeTracker.CQRS.Departments.UpdateDepartment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTracker.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/departments")]
    public class DepartmentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("", Name = "GetDepartments")]
        public async Task<ActionResult<IEnumerable<DepartmentResponseDto>>> GetDepartments()
        {
            var response = await _mediator.Send(new GetDepartmentsQuery());
            return Ok(response);
        }

        [HttpGet("{departmentId}", Name = "GetDepartment")]
        public async Task<ActionResult<DepartmentResponseDto>> GetDepartment([FromRoute] Guid departmentId)
        {
            var response = await _mediator.Send(new GetDepartmentQuery(departmentId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("", Name = "CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{departmentId}", Name = "UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid departmentId, [FromBody] UpdateDepartmentCommand command)
        {
            command.DepartmentId = departmentId;
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{departmentId}", Name = "DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid departmentId)
        {
            await _mediator.Send(new DeleteDepartmentCommand(departmentId));
            return Created();
        }
    }
}
