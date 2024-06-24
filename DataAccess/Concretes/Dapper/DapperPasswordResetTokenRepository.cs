using System.Linq.Expressions;
using Core.Entities.Concretes;
using Core.Extensions.Paging;
using Dapper;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes.Dapper;

public class DapperPasswordResetTokenRepository(DapperDatabaseContext context) : IPasswordResetTokenRepository
{
    public void Add(PasswordResetToken entity)
    {
        const string query = """
                             INSERT INTO PasswordResetTokens(Id,Token,Username,CreatedAt) 
                             VALUES(@Id,@Token,@Username,@CreatedAt)
                             """;
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@Token", entity.Token.Trim());
        parameters.Add("@Username", entity.Username.Trim());
        parameters.Add("@CreatedAt", entity.CreatedAt);
        var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public void Delete(PasswordResetToken entity)
    {
        const string query = "DELETE FROM PasswordResetTokens WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public PasswordResetToken? GetByToken(string token)
    {
        const string query = "SELECT * FROM PasswordResetTokens WHERE Token = @Token";
        var parameters = new DynamicParameters();
        parameters.Add("@Token", token.Trim());
        var connection = context.CreateConnection();
        var result = connection.QueryFirstOrDefault<PasswordResetToken>(query, parameters);
        connection.Close();
        return result;
    }

    public PasswordResetToken? GetByUsername(string username)
    {
        const string query = "SELECT * FROM PasswordResetTokens WHERE Username = @Username";
        var parameters = new DynamicParameters();
        parameters.Add("@Username", username.Trim());
        var connection = context.CreateConnection();
        var result = connection.QueryFirstOrDefault<PasswordResetToken>(query, parameters);
        connection.Close();
        return result;
    }


    public void Update(PasswordResetToken entity)
    {
        throw new InvalidOperationException("Update operation is not supported for password reset.");
    }

    public PasswordResetToken? Get(Expression<Func<PasswordResetToken, bool>> filter)
    {
        throw new InvalidOperationException("Filtering is not supported in dapper repository.");
    }

    public PageableModel<PasswordResetToken> GetList(int index, int size,
        Expression<Func<PasswordResetToken, bool>>? filter = null)
    {
        throw new InvalidOperationException("Get list operation is not supported for password reset.");
    }

    public PasswordResetToken? GetById(Guid id)
    {
        throw new InvalidOperationException("Get by id operation is not supported for password reset.");
    }
}