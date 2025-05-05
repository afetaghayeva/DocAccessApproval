using DocAccessApproval.Application.Features.UserFeatures.Dtos;
using DocAccessApproval.Application.Features.UserFeatures.Queries.GetRoles;
using DocAccessApproval.Domain.Exceptions;
using DocAccessApproval.Domain.Exceptions.UserExceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocAccessApproval.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRolesAsync()
    {
        var query = new GetRolesQuery();

        var result = await mediator.Send(query);
        return Ok(result);
    }
}
