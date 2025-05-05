using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler(IDocumentRepository documentRepository, IMapper mapper) 
    : IRequestHandler<UpdateDocumentCommand, DocumentDto>
{
    public async Task<DocumentDto> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetAsync(d => d.Id == request.Id, include: x => x.Include(x => x.AccessRequests));
        DocumentException.ThrowIfNull(document);
        document.CheckUserAccess(request.UserId, AccessType.Edit);

        var updatedDocument = documentRepository.Update(document);
        await documentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<DocumentDto>(updatedDocument);
        return result;
    }
}
