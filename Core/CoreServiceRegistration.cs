using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.IoC;
using Core.Mailing;
using Core.Security.Authorization;
using Microsoft.Extensions.DependencyInjection;
namespace Core;

public static class CoreServiceRegistration
{
    public static void AddCoreServices(this IServiceCollection services, params ICoreModule[] coreModules)
    {
        foreach (var coreModule in coreModules)
        {
            coreModule.Load(services);
        }

        services.AddSingleton<IMailService, MailKitMailManager>();
        services.AddSingleton<ICacheManager, MemoryCacheManager>();
        services.AddSingleton<ITokenHelper,JwtHelper>();
        
        ServiceTool.Create(services);
    }
}