using AutoMapper;
using DocAccessApproval.Application.Features.UserFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Application.Security.Hashing;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using DocAccessApproval.Domain.Exceptions.UserExceptions;
using MediatR;

namespace DocAccessApproval.Application.Features.UserFeatures.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<RegisterCommand, RegisteredUserDto>
{
    public async Task<RegisteredUserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(x => x.Email == request.Email);
        if (user is not null)
            throw new UserException("User with this email already exists");

        var userExistWithSameUserName = await userRepository.GetAsync(u => u.UserName == request.UserName);
        if (userExistWithSameUserName != null)
            throw new UserException("User with this username already exists");

        HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var newUser = new User(request.FirstName, request.LastName, request.Email, request.UserName, passwordSalt, passwordHash);

        var userRole = await roleRepository.GetAsync(x => x.Name == "user");
        newUser.AddUserRole(userRole);

        var addedUser = await userRepository.AddAsync(newUser);
        await userRepository.UnitOfWork.SaveChangesAsync();

        var registeredUser = mapper.Map<RegisteredUserDto>(addedUser);

        return registeredUser;
    }


}
