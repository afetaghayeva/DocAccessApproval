using DocAccessApproval.Application.Features.UserFeatures.Dtos;
using MediatR;

namespace DocAccessApproval.Application.Features.UserFeatures.Commands.Login;

public class LoginCommand:IRequest<LoggedUserDto>
{
    public LoginCommand(string emailOrUserName, string password)
    {
        EmailOrUserName = emailOrUserName;
        Password = password;
    }

    public string EmailOrUserName { get; init; }
    public string Password { get; init; }
}
