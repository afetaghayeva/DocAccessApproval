using DocAccessApproval.Application.Security.JWT;

namespace DocAccessApproval.Application.Features.UserFeatures.Dtos;

public class LoggedUserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public AccessToken AccessToken { get; set; }
    public List<RoleDto> Roles { get; set; } 
}
