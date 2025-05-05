using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.SeedWork.Paging;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetDocuments;

public class GetDocumentsQueryHandler(IDocumentRepository documentRepository, IMapper mapper)
    :IRequestHandler<GetDocumentsQuery, IPaginate<GetDocumentDto>>
{
    public async Task<IPaginate<GetDocumentDto>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = await documentRepository.GetAllAsPaginateAsync(index: request.Index, size: request.Size);

        var result = mapper.Map<Paginate<GetDocumentDto>>(documents);
        return result;
    }
}
