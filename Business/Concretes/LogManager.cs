using Business.Abstracts;
using Business.BusinessRules;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concretes;
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

    [CacheRemoveAspect("ILogService.Get")]
    public void DeleteLog(Guid logId)
    {
        var log = logBusinessRules.LogMustBeExist(logId);
        logRepository.Delete(log);
    }

    [CacheRemoveAspect("ILogService.Get")]
    public void DeleteAllLogs()
    {
        logRepository.DeleteAllLogs();
    }

    [CacheRemoveAspect("ILogService.Get")]
    public void DeleteLogRange(List<int> logIds)
    {
        logRepository.DeleteLogRange(logIds);
    }

    [CacheAspect]
    public PageableModel<Log> GetAllLogs(int index = 1, int size = 10)
    {
        return logRepository.GetList(index,size);
    }

    [CacheAspect]
    public PageableModel<Log> GetLogsByUserId(int userId, int index = 1, int size = 10)
    {
        return logRepository.GetLogsByUserId(userId, index, size);
    }

    [CacheAspect]
    public PageableModel<Log> GetLogsByLogLevel(string logLevel, int index = 1, int size = 10)
    {
        return logRepository.GetLogsByLogLevel(logLevel, index, size);
    }

    [CacheAspect]
    public PageableModel<Log> GetLogsByException(string exception, int index = 1, int size = 10)
    {
        return logRepository.GetLogsByException(exception, index, size);
    }
}