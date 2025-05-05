using DocAccessApproval.Domain.Exceptions.UserExceptions;
using DocAccessApproval.Domain.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAccessApproval.Domain.AggregateModels.UserAggregate;

public class UserRole : BaseEntity
{
    public UserRole()
    {

    }
    public UserRole(Guid id) : this()
    {
        Id = id;
    }
    public UserRole(Guid id, Guid userId, Guid roleId) : this(id)
    {
        UserException.ThrowIfDefault(userId);
        UserException.ThrowIfDefault(roleId);

        UserId = userId;
        RoleId = roleId;
    }
    public UserRole(Guid userId, Guid roleId) : this(Guid.NewGuid(), userId, roleId)
    {

    }

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }
}
