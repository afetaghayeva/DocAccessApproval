using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using MediatR;
using System.Text.Json.Serialization;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.CreateAccessRequest;

public class CreateAccessRequestCommand:IRequest<AccessRequestDto>
{
    public CreateAccessRequestCommand(Guid documentId, string reason, DateTime expireDate, List<AccessType> accessTypes)
    {
        DocumentId = documentId;
        Reason = reason;
        ExpireDate = expireDate;
        AccessTypes = accessTypes;
    }
    public Guid DocumentId { get; init; }
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string? Reason { get; init; }
    public DateTime ExpireDate { get; init; }
    public List<AccessType> AccessTypes { get; init; }
}
