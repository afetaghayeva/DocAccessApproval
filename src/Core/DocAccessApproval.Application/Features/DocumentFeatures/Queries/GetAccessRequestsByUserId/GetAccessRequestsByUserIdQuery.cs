using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.SeedWork.Paging;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetAccessRequestsByUserId;

public class GetAccessRequestsByUserIdQuery : IRequest<IPaginate<AccessRequestDto>>
{
    public GetAccessRequestsByUserIdQuery(Guid userId, int index, int size, RequestStatus? requestStatus)
    {
        UserId = userId;
        Index = index;
        Size = size;
        RequestStatus = requestStatus;
    }
    public Guid UserId { get; init; }
    public int Index { get; init; }
    public int Size { get; init; }
    public RequestStatus? RequestStatus { get; init; }
}
