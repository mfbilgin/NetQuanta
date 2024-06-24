using Core.DataAccess;
using Core.Entities.Concretes;
using Core.Extensions.Paging;
using Core.Logging;

namespace DataAccess.Abstracts;

public interface ILogRepository : IEntityRepository<Log>
{
    public void DeleteAllLogs();
    public void DeleteLogRange(List<Guid> logIds);
    public PageableModel<Log> GetLogsByRequestedValue(string requestedValue, int index = 1, int size = 10);
    public PageableModel<Log> GetLogsByLogLevel(string logLevel, int index = 1, int size = 10);
    public PageableModel<Log> GetLogsByException(string exception, int index = 1, int size = 10);
}