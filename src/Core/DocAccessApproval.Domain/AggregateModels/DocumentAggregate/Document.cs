using DocAccessApproval.Domain.AggregateModels.DocumentAggregate.Events;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using DocAccessApproval.Domain.SeedWork;

namespace DocAccessApproval.Domain.AggregateModels.DocumentAggregate;

public class Document : AggregateRoot
{
    public Document()
    {
        _accessRequests = new List<AccessRequest>();
    }
    public Document(Guid id) : this()
    {
        Id = id;
    }

    public Document(Guid id, string name, byte[] content) : this(id)
    {
        DocumentException.ThrowIfNullOrEmpty(name);
        DocumentException.ThrowIfNull(content);

        Name = name;
        Content = content;
    }

    public Document(string name, byte[] content) : this(Guid.NewGuid(), name, content)
    {

    }

    public string Name { get; private set; }
    public byte[] Content { get; private set; }

    private List<AccessRequest> _accessRequests;
    public IReadOnlyCollection<AccessRequest> AccessRequests => _accessRequests.AsReadOnly();

    public AccessRequest AddAccessRequest(Guid userId, int accessType, string reason, DateTime expireDate)
    {
        if (_accessRequests.Any(ar => ar.UserId == userId && ar.AccessType == accessType && ar.RequestStatus == RequestStatus.Pending))
            throw new DocumentException("An access request with the same type is already pending.");

        var accessRequest = new AccessRequest(Id, userId, accessType, reason, expireDate);
        _accessRequests.Add(accessRequest);

        return accessRequest;
    }

    public Decision SetDecision(Guid accessRequestId, Guid userId, bool isApproved, string comment)
    {
        var accessRequest = _accessRequests.FirstOrDefault(x => x.Id == accessRequestId);
        DocumentException.ThrowIfNull(accessRequest);

        accessRequest.SetRequestStatus(isApproved ? RequestStatus.Approved : RequestStatus.Declined);
        var decision = new Decision(userId, accessRequestId, comment);
        accessRequest.SetDecision(decision);

        AddDomainEvent(new DecisionMadeEvent(accessRequestId, userId, accessRequest.RequestStatus));

        return decision;
    }

    public void CheckUserAccess(Guid userId, AccessType accessType)
    {
        var userAccess = _accessRequests.FirstOrDefault(x => x.UserId == userId && x.ExpireDate >= DateTime.UtcNow
                                                        && x.RequestStatus == RequestStatus.Approved);
        DocumentException.ThrowIfNull(userAccess);

        if ((userAccess.AccessType & (int)accessType) != (int)accessType)//0011  0001
            throw new DocumentException("User does not have access to this document");
    }
}
