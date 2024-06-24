using Business.BusinessRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Security;
using Core.Extensions.Paging;
using Core.Logging;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class LogManager(ILogRepository logRepository,LogBusinessRules logBusinessRules) : ILogService
{
    [CacheRemoveAspect("ILogService.Get")]
    public void Log(Log log)
    {
        logRepository.Add(log);
    }

    [SecurityAspect("admin")]
    [CacheRemoveAspect("ILogService.Get")]
    public void DeleteLog(Guid logId)
    {
        var log = logBusinessRules.LogMustBeExist(logId);
        logRepository.Delete(log);
    }

    [SecurityAspect("admin")]
    [CacheRemoveAspect("ILogService.Get")]
    public void DeleteAllLogs()
    {
        logRepository.DeleteAllLogs();
    }

    [SecurityAspect("admin")]
    [CacheRemoveAspect("ILogService.Get")]
    public void DeleteLogRange(List<Guid> logIds)
    {
        logRepository.DeleteLogRange(logIds);
    }

    [SecurityAspect("admin")]
    [CacheAspect]
    public PageableModel<Log> GetAllLogs(int index = 1, int size = 10)
    {
        return logRepository.GetList(index,size);
    }

    [SecurityAspect("admin")]
    [CacheAspect]
    public PageableModel<Log> GetLogsByRequestedValue(string requestedValue, int index = 1, int size = 10)
    {
        return logRepository.GetLogsByRequestedValue(requestedValue, index, size);
    }

    [SecurityAspect("admin")]
    [CacheAspect]
    public PageableModel<Log> GetLogsByLogLevel(string logLevel, int index = 1, int size = 10)
    {
        return logRepository.GetLogsByLogLevel(logLevel, index, size);
    }

    [SecurityAspect("admin")]
    [CacheAspect]
    public PageableModel<Log> GetLogsByException(string exception, int index = 1, int size = 10)
    {
        return logRepository.GetLogsByException(exception, index, size);
    }
}