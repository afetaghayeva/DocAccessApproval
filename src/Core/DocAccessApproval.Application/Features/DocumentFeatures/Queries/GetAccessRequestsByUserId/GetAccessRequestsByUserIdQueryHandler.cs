using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.SeedWork.Paging;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetAccessRequestsByUserId;

public class GetAccessRequestsByUserIdQueryHandler(IDocumentRepository documentRepository, IMapper mapper)
    : IRequestHandler<GetAccessRequestsByUserIdQuery, IPaginate<AccessRequestDto>>
{
    public async Task<IPaginate<AccessRequestDto>> Handle(GetAccessRequestsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var accessRequests = await documentRepository.GetAccessRequestsByUserIdAsync(request.UserId, request.Index, request.Size, request.RequestStatus);

        var accessRequestDtos = mapper.Map<Paginate<AccessRequestDto>>(accessRequests);
        return accessRequestDtos;
    }
}
