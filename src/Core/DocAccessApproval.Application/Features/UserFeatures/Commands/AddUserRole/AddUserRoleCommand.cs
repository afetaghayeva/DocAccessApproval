using MediatR;

namespace DocAccessApproval.Application.Features.UserFeatures.Commands.AddUserRole;

public class AddUserRoleCommand : IRequest<bool>
{
    public AddUserRoleCommand(Guid userId,Guid roleId)
    {
        RoleId = roleId;
        UserId = userId;
    }

    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
}
