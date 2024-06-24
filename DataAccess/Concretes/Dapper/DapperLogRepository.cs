using System.Linq.Expressions;
using Core.Extensions.Paging;
using Core.Logging;
using Dapper;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using InvalidOperationException = System.InvalidOperationException;

namespace DataAccess.Concretes.Dapper;

public class DapperLogRepository(DapperDatabaseContext context) : ILogRepository
{
    public void DeleteAllLogs()
    {
        const string query = "DELETE FROM Logs";
        using var connection = context.CreateConnection();
        connection.Execute(query);
        connection.Close();
    }

    public void DeleteLogRange(List<Guid> logIds)
    {
        const string query = "DELETE FROM Logs WHERE Id IN @Ids";
        var parameters = new { Ids = logIds };
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public PageableModel<Log> GetLogsByRequestedValue(string requestedValue, int index = 1, int size = 10)
    {
        const string query = "SELECT * FROM Logs WHERE RequestedValue = @RequestedValue ORDER BY TimeStamp DESC";
        var parameters = new DynamicParameters();
        parameters.Add("@RequestedValue", requestedValue.Trim());
        using var connection = context.CreateConnection();
        var logs = connection.Query<Log>(query, parameters).ToList();
        connection.Close();
        return logs.ToPaginate(index, size);
    }

    public PageableModel<Log> GetLogsByLogLevel(string logLevel, int index = 1, int size = 10)
    {
        const string query = "SELECT * FROM Logs WHERE LogLevel = @LogLevel ORDER BY TimeStamp DESC";
        var parameters = new DynamicParameters();
        parameters.Add("@LogLevel", logLevel.Trim());
        using var connection = context.CreateConnection();
        var logs = connection.Query<Log>(query, parameters).ToList();
        connection.Close();
        return logs.ToPaginate(index, size);
    }

    public PageableModel<Log> GetLogsByException(string exception, int index = 1, int size = 10)
    {
        const string query = "SELECT * FROM Logs WHERE Exception = @Exception ORDER BY TimeStamp DESC";
        var parameters = new DynamicParameters();
        parameters.Add("@Exception", exception.Trim());
        using var connection = context.CreateConnection();
        var logs = connection.Query<Log>(query, parameters).ToList();
        connection.Close();
        return logs.ToPaginate(index, size);
    }

    public void Add(Log entity)
    {
        const string query =
            "INSERT INTO Logs(Id,RequestedValue,LogLevel,Message,Exception,TimeStamp,Source,Details) VALUES(@Id,@RequestedValue, @LogLevel, @Message, @Exception, @TimeStamp, @Source, @Details)";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@RequestedValue", entity.RequestedValue?.Trim());
        parameters.Add("@LogLevel", entity.LogLevel.Trim());
        parameters.Add("@Message", entity.Message.Trim());
        parameters.Add("@Exception", entity.Exception.Trim());
        parameters.Add("@TimeStamp", entity.TimeStamp);
        parameters.Add("@Source", entity.Source.Trim());
        parameters.Add("@Details", entity.Details?.Trim());
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public void Update(Log entity)
    {
        throw new InvalidOperationException("Update operation is not supported for logs.");
    }

    public void Delete(Log entity)
    {
        const string query = "DELETE FROM Logs WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public Log Get(Expression<Func<Log, bool>> filter)
    {
        throw new InvalidOperationException("Filtering is not supported in dapper repository.");
    }

    public Log? GetById(Guid id)
    {
        const string query = "SELECT * FROM Logs WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        using var connection = context.CreateConnection();
        var log = connection.QueryFirstOrDefault<Log>(query, parameters);
        connection.Close();
        return log;
    }

    public PageableModel<Log> GetList(int index, int size, Expression<Func<Log, bool>>? filter = null)
    {
        if (filter is not null) throw new InvalidOperationException("Filtering is not supported in dapper repository.");
        const string query = "SELECT * FROM Logs ORDER BY TimeStamp DESC";
        using var connection = context.CreateConnection();
        var logs = connection.Query<Log>(query).ToList();
        connection.Close();
        return logs.ToPaginate(index, size);
    }
}