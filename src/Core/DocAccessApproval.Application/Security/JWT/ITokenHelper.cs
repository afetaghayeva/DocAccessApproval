using DocAccessApproval.Domain.AggregateModels.UserAggregate;

namespace DocAccessApproval.Application.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IEnumerable<Role> roles);
}