namespace DocAccessApproval.Application.Security.JWT;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}