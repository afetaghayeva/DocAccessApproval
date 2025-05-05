using DocAccessApproval.Application.Features.UserFeatures.Commands.AddUserRole;
using DocAccessApproval.Application.Features.UserFeatures.Commands.RemoveUserRole;
using DocAccessApproval.Domain.Exceptions;
using DocAccessApproval.Domain.Exceptions.UserExceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocAccessApproval.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRolesController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAsync([FromBody] AddUserRoleCommand command)
    {
        var result = await mediator.Send(command);

        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{roleId}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveAsync([FromBody] RemoveUserRoleCommand command, [FromRoute] Guid roleId)
    {
        command.RoleId = roleId;
        var result = await mediator.Send(command);

        return Ok(result);
    }
}
