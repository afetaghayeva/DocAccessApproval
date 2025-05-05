namespace DocAccessApproval.Application.Features.UserFeatures.Dtos;

public class RegisteredUserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
}
