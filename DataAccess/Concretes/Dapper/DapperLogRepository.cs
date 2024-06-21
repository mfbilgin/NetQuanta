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
    const string query = "DELETE FROM Logs ORDER BY TimeStamp DESC";
        using var connection = context.CreateConnection();
        connection.Execute(query);
        connection.Close();
    }

    public void DeleteLogRange(List<int> logIds)
    {
        const string query = "DELETE FROM Logs WHERE Id in (@Id) ORDER BY TimeStamp DESC";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", logIds);
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public PageableModel<Log> GetLogsByUserId(int userId, int index = 1, int size = 10)
    {
        const string query = "SELECT * FROM Logs WHERE UserId = @UserId ORDER BY TimeStamp DESC";
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        using var connection = context.CreateConnection();
        var logs = connection.Query<Log>(query, parameters).ToList();
        connection.Close();
        return logs.ToPaginate(index, size);
    }

    public PageableModel<Log> GetLogsByLogLevel(string logLevel, int index = 1, int size = 10)
    {
        const string query = "SELECT * FROM Logs WHERE LogLevel = @LogLevel ORDER BY TimeStamp DESC";
        var parameters = new DynamicParameters();
        parameters.Add("@LogLevel", logLevel);
        using var connection = context.CreateConnection();
        var logs = connection.Query<Log>(query, parameters).ToList();
        connection.Close();
        return logs.ToPaginate(index, size);
    }

    public PageableModel<Log> GetLogsByException(string exception, int index = 1, int size = 10)
    {
        const string query = "SELECT * FROM Logs WHERE Exception = @Exception ORDER BY TimeStamp DESC";
        var parameters = new DynamicParameters();
        parameters.Add("@Exception", exception);
        using var connection = context.CreateConnection();
        var logs = connection.Query<Log>(query, parameters).ToList();
        connection.Close();
        return logs.ToPaginate(index, size);
    }

    public void Add(Log entity)
    {
        const string query =
            "INSERT INTO Logs(Id,Username,LogLevel,Message,Exception,TimeStamp,Source,Details) VALUES(@Id,@Username, @LogLevel, @Message, @Exception, @TimeStamp, @Source, @Details)";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@Username", entity.UserName);
        parameters.Add("@LogLevel", entity.LogLevel);
        parameters.Add("@Message", entity.Message);
        parameters.Add("@Exception", entity.Exception);
        parameters.Add("@TimeStamp", entity.TimeStamp);
        parameters.Add("@Source", entity.Source);
        parameters.Add("@Details", entity.Details);
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