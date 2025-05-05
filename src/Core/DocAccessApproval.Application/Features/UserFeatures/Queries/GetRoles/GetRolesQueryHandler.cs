using AutoMapper;
using DocAccessApproval.Application.Features.UserFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using MediatR;

namespace DocAccessApproval.Application.Features.UserFeatures.Queries.GetRoles;

public class GetRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleRepository.GetAllAsync();

        var result=mapper.Map<List<RoleDto>>(roles);
        return result;
    }
}
