using Core.Extensions.Paging;

namespace Core.Logging;

public interface ILogService
{
    public void Log(Log log);
    public void DeleteLog(Guid logId);
    public void DeleteAllLogs();
    public void DeleteLogRange(List<Guid> logIds);
    public PageableModel<Log> GetAllLogs(int index = 1, int size = 10);
    public PageableModel<Log> GetLogsByUsername(string username, int index = 1, int size = 10);
    public PageableModel<Log> GetLogsByLogLevel(string logLevel, int index = 1, int size = 10);
    public PageableModel<Log> GetLogsByException(string exception, int index = 1, int size = 10);
    
}