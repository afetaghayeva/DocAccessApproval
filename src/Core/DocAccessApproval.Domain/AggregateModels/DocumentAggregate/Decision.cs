using DocAccessApproval.Domain.SeedWork;

namespace DocAccessApproval.Domain.AggregateModels.DocumentAggregate;

public class Decision : BaseEntity
{
    public Decision()
    {
        DecisionDate = DateTime.UtcNow;
    }
    public Decision(Guid id) : this()
    {
        Id = id;
    }
    public Decision(Guid userId, Guid accessRequestId, string comment) : this(Guid.NewGuid())
    {
        UserId = userId;
        AccessRequestId = accessRequestId;
        Comment = comment;
    }

    public Guid UserId { get; private set; }
    public Guid AccessRequestId { get; private set; }
    public string Comment { get; private set; }
    public DateTime DecisionDate { get; private set; }
}
