using DocAccessApproval.Application.Extensions;
using DocAccessApproval.Application.Features.DocumentFeatures.Commands.CreateAccessRequest;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetAccessRequestsByUserId;
using DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetAllAccessRequests;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.Exceptions;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using DocAccessApproval.Domain.SeedWork.Paging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocAccessApproval.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessRequestsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "approver")]
    [ProducesResponseType(typeof(IPaginate<AccessRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync([FromQuery] int index, [FromQuery] int size, [FromQuery] RequestStatus requestStatus = RequestStatus.Pending)
    {
        var query = new GetAllAccessRequestsQuery(index, size, requestStatus);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{userId}")]
    [Authorize]
    [ProducesResponseType(typeof(IPaginate<AccessRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccessRequestsByUserIdAsync([FromRoute] Guid userId, [FromQuery] int index, [FromQuery] int size, [FromQuery] RequestStatus requestStatus = RequestStatus.Pending)
    {
        var query = new GetAccessRequestsByUserIdQuery(userId, index, size, requestStatus);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(AccessRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAsync([FromBody] CreateAccessRequestCommand createAccessRequestCommand)
    {
        createAccessRequestCommand.UserId = User.GetUserId();
        var result = await mediator.Send(createAccessRequestCommand);

        return Ok(result);
    }
}
