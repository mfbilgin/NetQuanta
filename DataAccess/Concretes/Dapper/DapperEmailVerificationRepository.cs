using System.Linq.Expressions;
using Core.Entities.Concretes;
using Core.Extensions.Paging;
using Dapper;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes.Dapper;

public sealed class DapperEmailVerificationRepository(DapperDatabaseContext context) : IEmailVerificationRepository
{
    public void Add(EmailVerification entity)
    {
        const string query = "INSERT INTO EmailVerifications (Id, Username, Token,CreatedAt) VALUES (@Id, @Username, @Token, @CreatedAt)";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@Username", entity.Username);
        parameters.Add("@Token", entity.Token);
        parameters.Add("@CreatedAt", entity.CreatedAt);
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public void Update(EmailVerification entity)
    {
        const string query = "UPDATE EmailVerifications SET Username = @Username, Token = @Token WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@Username", entity.Username);
        parameters.Add("@Token", entity.Token);
        
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public void Delete(EmailVerification entity)
    {
        const string query = "DELETE FROM EmailVerifications WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public EmailVerification? Get(Expression<Func<EmailVerification, bool>> filter)
    {
        throw new InvalidOperationException("Filtering is not supported in dapper repository.");
    }

    public EmailVerification? GetById(Guid id)
    {
        const string query = "SELECT * FROM EmailVerifications WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        using var connection = context.CreateConnection();
        var emailVerification = connection.QueryFirstOrDefault<EmailVerification>(query, parameters);
        connection.Close();
        return emailVerification;
    }

    public PageableModel<EmailVerification> GetList(int index, int size,
        Expression<Func<EmailVerification, bool>>? filter = null)
    {
        if (filter is not null)
        {
            throw new InvalidOperationException("Filtering is not supported in dapper repository.");
        }
        const string query = "SELECT * FROM EmailVerifications";
        using var connection = context.CreateConnection();
        var emailVerifications = connection.Query<EmailVerification>(query).ToList();
        connection.Close();
        return emailVerifications.ToPaginate(index, size);
    }

    public EmailVerification? GetByToken(string token)
    {
        const string query = "SELECT * FROM EmailVerifications WHERE Token = @Token";
        var parameters = new DynamicParameters();
        parameters.Add("@Token", token);
        using var connection = context.CreateConnection();
        var emailVerification = connection.QueryFirstOrDefault<EmailVerification>(query, parameters);
        connection.Close();
        return emailVerification;
    }

    public EmailVerification? GetByUsername(string username)
    {
        const string query = "SELECT * FROM EmailVerifications WHERE Username = @Username";
        var parameters = new DynamicParameters();
        parameters.Add("@Username", username);
        using var connection = context.CreateConnection();
        var emailVerification = connection.QueryFirstOrDefault<EmailVerification>(query, parameters);
        connection.Close();
        return emailVerification;
    }
}