using DocAccessApproval.Application.Extensions;
using DocAccessApproval.Application.Features.DocumentFeatures.Commands.AddDocument;
using DocAccessApproval.Application.Features.DocumentFeatures.Commands.DeleteDocument;
using DocAccessApproval.Application.Features.DocumentFeatures.Commands.UpdateDocument;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetDocumentById;
using DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetDocuments;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using DocAccessApproval.Domain.Exceptions;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using DocAccessApproval.Domain.SeedWork.Paging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocAccessApproval.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IPaginate<DocumentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync([FromQuery] int index, [FromQuery] int size)
    {
        var query = new GetDocumentsQuery(index, size);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(IPaginate<DocumentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var userId = User.GetUserId();
        var query = new GetDocumentByIdQuery(id, userId);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "approver")]
    [ProducesResponseType(typeof(DocumentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAsync(IFormFile file, [FromQuery] string name)
    {
        var command = new AddDocumentCommand(name, file);
        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(DocumentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(IFormFile file, [FromRoute] Guid id, [FromQuery] string name)
    {
        var userId = User.GetUserId();
        var command = new UpdateDocumentCommand(id, name, file, userId);
        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DocumentProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteDocumentCommand(id, User.GetUserId());
        var result = await mediator.Send(command);

        return Ok(result);
    }
}
