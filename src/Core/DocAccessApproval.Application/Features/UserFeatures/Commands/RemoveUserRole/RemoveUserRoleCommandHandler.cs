using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.Exceptions.UserExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocAccessApproval.Application.Features.UserFeatures.Commands.RemoveUserRole;

public class RemoveUserRoleCommandHandler(IUserRepository userRepository):IRequestHandler<RemoveUserRoleCommand, bool>
{
    public async Task<bool> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(x => x.Id == request.UserId, include: x => x.Include(x=>x.UserRoles));
        UserException.ThrowIfNull(user);

        user.RemoveUserRole(request.RoleId);
        userRepository.Update(user);
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
