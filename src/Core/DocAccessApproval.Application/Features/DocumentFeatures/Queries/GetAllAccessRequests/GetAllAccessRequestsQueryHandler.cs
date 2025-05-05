using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.SeedWork.Paging;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetAllAccessRequests;

public class GetAllAccessRequestsQueryHandler(IDocumentRepository documentRepository, IMapper mapper)
    : IRequestHandler<GetAllAccessRequestsQuery, IPaginate<AccessRequestDto>>
{
    public async Task<IPaginate<AccessRequestDto>> Handle(GetAllAccessRequestsQuery request, CancellationToken cancellationToken)
    {
        var accessRequests = await documentRepository.GetAllAccessRequestsAsync(request.Index, request.Size, request.RequestStatus);
        var accessRequestDtos = mapper.Map<Paginate<AccessRequestDto>>(accessRequests);
        return accessRequestDtos;
    }
}
