using DocAccessApproval.Application.Features.UserFeatures.Dtos;
using MediatR;

namespace DocAccessApproval.Application.Features.UserFeatures.Commands.Register;

public class RegisterCommand:IRequest<RegisteredUserDto>
{
    public RegisterCommand(string firstName, string lastName, string userName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        Password = password;
    }

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }



}
