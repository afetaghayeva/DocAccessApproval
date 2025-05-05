using DocAccessApproval.Application.Features.UserFeatures.Commands.Login;
using DocAccessApproval.Application.Features.UserFeatures.Commands.Register;
using DocAccessApproval.Application.Features.UserFeatures.Dtos;
using DocAccessApproval.Domain.Exceptions;
using DocAccessApproval.Domain.Exceptions.UserExceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocAccessApproval.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController(IMediator mediator) : ControllerBase
{
    [HttpPost("Register")]
    [ProducesResponseType(typeof(RegisteredUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command)
    {
        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(LoggedUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);

        return Ok(result);
    }
}
