using Asp.Versioning;
using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Features.Departments.CreateDepartment;
using EmployeeManager.Application.Features.Departments.DeleteDepartment;
using EmployeeManager.Application.Features.Departments.GetDepartment;
using EmployeeManager.Application.Features.Departments.GetDepartmentEmployees;
using EmployeeManager.Application.Features.Departments.GetDepartments;
using EmployeeManager.Application.Features.Departments.UpdateDepartment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Web.Controllers
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

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost("", Name = "CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut("{departmentId}", Name = "UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid departmentId, [FromBody] UpdateDepartmentCommand command)
        {
            command.DepartmentId = departmentId;
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("{departmentId}", Name = "DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid departmentId)
        {
            await _mediator.Send(new DeleteDepartmentCommand(departmentId));
            return Ok();
        }

        [HttpGet("{departmentId}/employees", Name = "GetDepartmentEmployees")]
        public async Task<ActionResult<IEnumerable<DepartmentEmployeeResponseDto>>> GetDepartmentEmployees([FromRoute] Guid departmentId)
        {
            var response = await _mediator.Send(new GetDepartmentEmployeesQuery(departmentId));
            return Ok(response);
        }
    }
}
