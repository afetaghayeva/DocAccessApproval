using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.AddDocument;

public class AddDocumentCommand:IRequest<DocumentDto>
{
    public AddDocumentCommand(string name, IFormFile content)
    {
        Name = name;
        Content = content;
    }

    public string Name { get; init; }
    public IFormFile Content { get; init; }
}
