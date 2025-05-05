using DocAccessApproval.Application.Extensions;
using DocAccessApproval.Application.Features.DocumentFeatures.Commands.MakeDecision;
using DocAccessApproval.Domain.Exceptions;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocAccessApproval.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DecisionsController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles ="approver")]
    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAsync([FromBody] MakeDecisionCommand makeDecisionCommand)
    {
        makeDecisionCommand.UserId=User.GetUserId();
        var result = await mediator.Send(makeDecisionCommand);

        return Ok(result);
    }
}
