using Castle.DynamicProxy;
using Core.Exceptions;
using Core.Interceptors;
using Core.Security.Authorization;

namespace Core.Aspects.Autofac.Security;

public class SecurityAspect(string roles) : MethodInterception
{
    private readonly string[] _roles = roles.Split(',');
    
    protected override void OnBefore(IInvocation invocation)
    {
        
        var roleClaims = JwtHelper.GetAuthenticatedUserRoles();
        if (_roles[0] == "all" && roleClaims.Count != 0) return;
        if (_roles.Any(role => roleClaims.Contains(role)))
        {
            return;
        }

        throw new AuthorizationException();
    }
}