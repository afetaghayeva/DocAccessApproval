using System.Security.Claims;

namespace DocAccessApproval.Application.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        List<string>? result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        return result;
    }

    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.Role);
    }

    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userIdString = claimsPrincipal?.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault();
        Guid userId=String.IsNullOrEmpty(userIdString) ? Guid.Empty : Guid.Parse(userIdString);

        return userId;
    }

    public static string? GetUserFirstName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.Name).FirstOrDefault();
    }

    public static string? GetUserSurname(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.Surname).FirstOrDefault();
    }
}