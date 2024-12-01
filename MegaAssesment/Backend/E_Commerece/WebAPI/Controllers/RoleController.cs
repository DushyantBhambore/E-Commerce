using App.Core.Apps.Role.Command;
using App.Core.Apps.Role.Query;
using App.Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
        {
            var result = await _mediator.Send(new AddRoleCommand { RoleDto = roleDto });
            return Ok(result);
        }
        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleDto roleDto)
        {
            var result = await _mediator.Send(new UpdateRoleCommand { RoleDto = roleDto });
            return Ok(result);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRole([FromBody] int id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand { id = id });
            return Ok(result);
        }
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRoleQuery());
            return Ok(result);
        }
        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById([FromQuery] int id)
        {
            var result = _mediator.Send(new GetRoleByIdQuery { id = id });
            return Ok(result);
        }
    }
}
