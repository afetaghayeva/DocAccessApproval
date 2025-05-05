using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.SeedWork.Paging;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetAllAccessRequests;

public class GetAllAccessRequestsQuery : IRequest<IPaginate<AccessRequestDto>>
{
    public GetAllAccessRequestsQuery(int index, int size, RequestStatus requestStatus)
    {
        Index = index;
        Size = size;
        RequestStatus = requestStatus;
    }

    public int Index { get; init; }
    public int Size { get; init; }
    public RequestStatus RequestStatus { get; init; }
}
