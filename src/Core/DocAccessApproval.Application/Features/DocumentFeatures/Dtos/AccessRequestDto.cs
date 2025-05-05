using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Dtos;

public class AccessRequestDto
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public Guid UserId { get; set; }
    public string Reason { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public AccessType AccessType { get; set; }
    public RequestStatus RequestStatus { get; set; }
}
