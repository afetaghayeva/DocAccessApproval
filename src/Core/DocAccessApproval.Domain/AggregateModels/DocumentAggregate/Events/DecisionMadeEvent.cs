using DocAccessApproval.Domain.SeedWork;

namespace DocAccessApproval.Domain.AggregateModels.DocumentAggregate.Events;

public class DecisionMadeEvent : IDomainEvent
{
    public DecisionMadeEvent(Guid accessRequestId, Guid approvedByUserId, RequestStatus requestStatus)
    {
        AccessRequestId = accessRequestId;
        ApprovedByUserId = approvedByUserId;
        RequestStatus = requestStatus;
    }
    public Guid AccessRequestId { get; }
    public Guid ApprovedByUserId { get; }
    public RequestStatus RequestStatus { get; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}
