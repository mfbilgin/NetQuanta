using DataAccess.Abstracts;
using DataAccess.Concretes.Dapper;
using DataAccess.Concretes.EntityFramework;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DataAccessServiceRegistration
{
    public static void AddDataAccessServices(this IServiceCollection services)
    {
        //Log only has a Dapper implementation. Not remove this line.
        services.AddSingleton<ILogRepository, DapperLogRepository>();
        
        services.AddSingleton<IRoleRepository, DapperRoleRepository>();
        services.AddSingleton<IUserRepository, DapperUserRepository>();
        services.AddSingleton<IEmailVerificationRepository, DapperEmailVerificationRepository>();
        services.AddSingleton<DapperDatabaseContext>();
    }
}