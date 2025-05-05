using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.UpdateDocument;

public class UpdateDocumentCommand:IRequest<DocumentDto>
{
    public UpdateDocumentCommand(Guid id,string name, IFormFile content, Guid userId)
    {
        Id = id;
        Name = name;
        Content = content;
        UserId = userId;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public IFormFile Content { get; init; }
    public Guid UserId { get; init; }
}
