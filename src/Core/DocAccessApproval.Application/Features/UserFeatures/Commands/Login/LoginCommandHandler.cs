using AutoMapper;
using DocAccessApproval.Application.Features.UserFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Application.Security.Hashing;
using DocAccessApproval.Application.Security.JWT;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using DocAccessApproval.Domain.Exceptions.UserExceptions;
using MediatR;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DocAccessApproval.Application.Features.UserFeatures.Commands.Login;

public class LoginCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IMapper mapper)
    : IRequestHandler<LoginCommand, LoggedUserDto>
{
    public async Task<LoggedUserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(x => x.Email == request.EmailOrUserName || x.UserName == request.EmailOrUserName,
                                                include: x => x.Include(x => x.UserRoles).ThenInclude(x => x.Role));
        UserException.ThrowIfNull(user);

        if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new UserException("Password is incorrect");

        var roles = user.UserRoles.Select(x=>x.Role);

        AccessToken accessToken = tokenHelper.CreateToken(user, roles);
        var loggedUser = mapper.Map<LoggedUserDto>(user);
        loggedUser.AccessToken = accessToken;

        loggedUser.Roles = mapper.Map<List<RoleDto>>(roles);

        return loggedUser;
    }
}
