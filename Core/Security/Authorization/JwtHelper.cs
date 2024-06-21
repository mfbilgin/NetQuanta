using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Entities.Concretes;
using Core.Extensions.Claim;
using Core.IoC;
using Core.Security.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security.Authorization;

public sealed class JwtHelper(IConfiguration configuration) : ITokenHelper
{
    private readonly TokenOptions _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>()!;
    private DateTime _accessTokenExpiration;
    private static readonly IHttpContextAccessor? HttpContextAccessor =
        ServiceTool.ServiceProvider?.GetService<IHttpContextAccessor>();
    public AccessToken CreateToken(User user,Role role)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials,role);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }

    public static Guid GetAuthenticatedUserId()
    {
        CheckHttpContextAccessor();
        var userId = HttpContextAccessor!.HttpContext!.User.UserId();
        return userId is not null ? Guid.Parse(userId) : Guid.Empty;
    }

    public static List<string> GetAuthenticatedUserRoles()
    {
        CheckHttpContextAccessor();
        return HttpContextAccessor!.HttpContext!.User.Roles();
    }
    
    public static string? GetAuthenticatedUsername()
    {
        CheckHttpContextAccessor();
        return HttpContextAccessor!.HttpContext!.User.Username();
    }
    
    private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
        SigningCredentials signingCredentials, Role role)
    {
        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, role),
            signingCredentials: signingCredentials
        );
        return jwt;
    }
    
    private IEnumerable<Claim> SetClaims(User user, Role role)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddName(user.Username);
        claims.AddRoles([role.Name]);
        return claims;
    }
    
    
    private static void CheckHttpContextAccessor()
    {
        if (HttpContextAccessor == null)
        {
            throw new NullReferenceException("HttpContextAccessor is null.");
        }
    }
}