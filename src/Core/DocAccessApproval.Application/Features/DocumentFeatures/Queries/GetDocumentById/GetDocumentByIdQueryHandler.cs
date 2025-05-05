using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetDocumentById;

public class GetDocumentByIdQueryHandler(IDocumentRepository documentRepository, IMapper mapper)
    : IRequestHandler<GetDocumentByIdQuery, DocumentDto>
{
    public async Task<DocumentDto> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetAsync(d => d.Id == request.Id, include: x => x.Include(x => x.AccessRequests));
        DocumentException.ThrowIfNull(document);
        document.CheckUserAccess(request.UserId, AccessType.Read);


        var result = mapper.Map<DocumentDto>(document);
        return result;
    }
}
