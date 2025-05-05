using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using DocAccessApproval.Domain.SeedWork;

namespace DocAccessApproval.Domain.AggregateModels.DocumentAggregate;

public class AccessRequest : BaseEntity
{
    public AccessRequest()
    {
        RequestDate = DateTime.UtcNow;
        SetRequestStatus(RequestStatus.Pending);
    }
    public AccessRequest(Guid id) : this()
    {
        Id = id;
    }
    public AccessRequest(Guid documentId, Guid userId, int accessType, string reason, DateTime expireDate)
        : this(Guid.NewGuid())
    {
        DocumentException.ThrowIfDefault(documentId);
        DocumentException.ThrowIfDefault(userId);
        DocumentException.ThrowIfDefault(expireDate);

        if (expireDate <= RequestDate)
            throw new DocumentException("Expire date must be greater than request date");


        DocumentId = documentId;
        UserId = userId;
        AccessType = accessType;
        Reason = reason;
        ExpireDate = expireDate;
    }

    public Guid DocumentId { get; private set; }
    public Guid UserId { get; private set; }
    public int AccessType { get; private set; }
    public string Reason { get; private set; }
    public DateTime RequestDate { get; private set; }
    public DateTime ExpireDate { get; private set; }
    public RequestStatus RequestStatus { get; private set; }

    public Decision Decision { get; private set; }

    public void SetDecision(Decision decision)
    {
        DocumentException.ThrowIfNull(decision);

        Decision = decision;
    }

    public void SetRequestStatus(RequestStatus requestStatus)
    {
        if (RequestStatus == RequestStatus.Approved)
            throw new DocumentException("Request is already approved");
        if (RequestStatus == RequestStatus.Declined)
            throw new DocumentException("Request is already declined");

        RequestStatus = requestStatus;
    }
}

public enum AccessType
{
    Read = 1,
    Edit = 2,
    Delete = 4
}
public enum RequestStatus
{
    Pending = 1,
    Approved = 2,
    Declined = 3
}

