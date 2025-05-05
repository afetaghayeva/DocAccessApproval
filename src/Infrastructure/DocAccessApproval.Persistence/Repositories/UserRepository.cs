using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using DocAccessApproval.Persistence.Context;

namespace DocAccessApproval.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DocAccessApprovalDbContext context) : base(context)
    {
    }
}
