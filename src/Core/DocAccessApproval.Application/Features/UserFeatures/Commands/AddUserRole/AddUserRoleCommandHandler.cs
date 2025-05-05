using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.Exceptions.UserExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocAccessApproval.Application.Features.UserFeatures.Commands.AddUserRole;

public class AddUserRoleCommandHandler(IUserRepository userRepository) : IRequestHandler<AddUserRoleCommand, bool>
{
    public async Task<bool> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(x => x.Id == request.UserId,  include: x => x.Include(x=>x.UserRoles));
        UserException.ThrowIfNull(user);

        user.AddUserRole(request.RoleId);
        userRepository.Update(user);
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
