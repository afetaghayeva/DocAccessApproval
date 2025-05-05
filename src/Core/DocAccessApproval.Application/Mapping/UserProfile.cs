using AutoMapper;
using DocAccessApproval.Application.Features.UserFeatures.Dtos;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;

namespace DocAccessApproval.Application.Mapping;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<User, RegisteredUserDto>();
        CreateMap<User, LoggedUserDto>();
        CreateMap<Role, RoleDto>();
    }
}
