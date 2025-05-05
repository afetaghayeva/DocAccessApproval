using MediatR;
using System.Text.Json.Serialization;

namespace DocAccessApproval.Application.Features.UserFeatures.Commands.RemoveUserRole;

public class RemoveUserRoleCommand:IRequest<bool>
{
    public RemoveUserRoleCommand(Guid userId)
    {
        UserId = userId;
    }
    public Guid UserId { get; init; }
    [JsonIgnore]
    public Guid RoleId { get; set; }
}
