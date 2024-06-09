using System.Security.Claims;
namespace Core.Extensions.Claim;

public static class ClaimExtensions
{
    public static void AddName(this ICollection<System.Security.Claims.Claim> claims, string name)
    {
        claims.Add(new System.Security.Claims.Claim("Username", name));
    }

    public static void AddNameIdentifier(this ICollection<System.Security.Claims.Claim> claims, string nameIdentifier)
    {
        claims.Add(new System.Security.Claims.Claim("UserId", nameIdentifier));
    }

    public static void AddRoles(this ICollection<System.Security.Claims.Claim> claims, string[] roles)
    {
        roles.ToList().ForEach(role => claims.Add(new System.Security.Claims.Claim("Role", role)));
    }
}