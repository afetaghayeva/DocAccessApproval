using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.DeleteDocument;

public class DeleteDocumentCommandHandler(IDocumentRepository documentRepository)
    : IRequestHandler<DeleteDocumentCommand, bool>
{
    public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetAsync(d => d.Id == request.Id, include:x=>x.Include(x=>x.AccessRequests));
        DocumentException.ThrowIfNull(document);
        document.CheckUserAccess(request.UserId, AccessType.Delete);

        documentRepository.Delete(document);
        await documentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
