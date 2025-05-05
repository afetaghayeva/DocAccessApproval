using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using MediatR;
using System.Text.Json.Serialization;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.MakeDecision;

public class MakeDecisionCommand:IRequest<DecisionDto>
{
    public MakeDecisionCommand(Guid accessRequestId, Guid userId, bool isApproved,string? comment)
    {
        AccessRequestId = accessRequestId;
        UserId = userId;
        IsApproved = isApproved;
        Comment = comment;
    }

    public Guid AccessRequestId { get; init; }
    [JsonIgnore]
    public Guid UserId { get; set; }
    public bool IsApproved { get; init; }
    public string? Comment { get; init; }
}
