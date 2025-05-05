using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using DocAccessApproval.Persistence.Context;

namespace DocAccessApproval.Persistence.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(DocAccessApprovalDbContext context) : base(context)
    {
    }
}
