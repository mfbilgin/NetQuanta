using System.Security.Claims;

namespace Core.Extensions.Claim;

public static class ClaimsPrincipalExtensions
{
    private static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var result = claimsPrincipal.FindAll(claimType).Select(x => x.Value).ToList();
        return result;
    }

    public static List<string> Roles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims("Role");
    }

    public static string UserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst("UserId")!.Value;
    }
}