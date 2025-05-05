using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.AddDocument;

public class AddDocumentCommandHandler(IDocumentRepository documentRepository, IMapper mapper)
    : IRequestHandler<AddDocumentCommand, DocumentDto>
{
    public async Task<DocumentDto> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
    {
        using var content=request.Content.OpenReadStream();
        using var memoryStream = new MemoryStream();
        await content.CopyToAsync(memoryStream, cancellationToken);
        var document = new Document(request.Name, memoryStream.ToArray());

        memoryStream.Position = 0;

        var addedDocument = await documentRepository.AddAsync(document);
        await documentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<DocumentDto>(addedDocument);
        return result;
    }
}
