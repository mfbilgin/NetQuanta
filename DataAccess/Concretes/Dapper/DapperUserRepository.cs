using System.Linq.Expressions;
using Core.Entities.Concretes;
using Core.Extensions.Paging;
using Dapper;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using InvalidOperationException = System.InvalidOperationException;

namespace DataAccess.Concretes.Dapper;

public class DapperUserRepository(DapperDatabaseContext context) : IUserRepository
{
    public void Add(User entity)
    {
        const string query = """
              INSERT INTO Users(Id,RoleId,FirstName,LastName,Email,IsEmailVerified,Username,PasswordHash,PasswordSalt) 
              VALUES(@Id,@RoleId,@FirstName,@LastName,@Email,@IsEmailVerified,@Username,@PasswordHash,@PasswordSalt)
              """;
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@RoleId", entity.RoleId);
        parameters.Add("@FirstName", entity.FirstName);
        parameters.Add("@LastName", entity.LastName);
        parameters.Add("@Email", entity.Email);
        parameters.Add("@IsEmailVerified", entity.IsEmailVerified);
        parameters.Add("@Username", entity.Username.ToLower());
        parameters.Add("@PasswordHash", entity.PasswordHash);
        parameters.Add("@PasswordSalt", entity.PasswordSalt);
        var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public void Update(User entity)
    {
        const string query = """
              UPDATE Users 
              SET RoleId = @RoleId, FirstName = @FirstName, LastName = @LastName, Email = @Email, IsEmailVerified = @IsEmailVerified, PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt
              WHERE Id = @Id
              """;
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@RoleId", entity.RoleId);
        parameters.Add("@FirstName", entity.FirstName);
        parameters.Add("@LastName", entity.LastName);
        parameters.Add("@Email", entity.Email);
        parameters.Add("@IsEmailVerified", entity.IsEmailVerified);
        parameters.Add("@PasswordHash", entity.PasswordHash);
        parameters.Add("@PasswordSalt", entity.PasswordSalt);
        var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public void Delete(User entity)
    {
        const string query = "DELETE FROM Users WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public User Get(Expression<Func<User, bool>> filter)
    {
        throw new InvalidOperationException("Filtering is not supported in dapper repository.");
    }

    public User? GetById(Guid id)
    {
        const string query = "SELECT * FROM Users WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        var connection = context.CreateConnection();
        var user = connection.QueryFirstOrDefault<User>(query, parameters);
        connection.Close();
        return user;
    }

    public PageableModel<User> GetList(int index, int size, Expression<Func<User, bool>>? filter = null)
    {
        if (filter is not null) throw new InvalidOperationException("Filtering is not supported in dapper repository.");
        const string query = "SELECT * FROM Users";
        var connection = context.CreateConnection();
        var users = connection.Query<User>(query).ToList();
        connection.Close();
        return users.ToPaginate(index, size);
    }

    public User? GetByUsername(string username)
    {
        const string query = "SELECT * FROM Users WHERE Username = @Username";
        var parameters = new DynamicParameters();
        parameters.Add("@Username", username);
        var connection = context.CreateConnection();
        var user = connection.QueryFirstOrDefault<User>(query, parameters);
        connection.Close();
        return user;
    }

    public User? GetByEmail(string email)
    {
        const string query = "SELECT * FROM Users WHERE Email = @Email";
        var parameters = new DynamicParameters();
        parameters.Add("@Email", email);
        var connection = context.CreateConnection();
        var user = connection.QueryFirstOrDefault<User>(query, parameters);
        connection.Close();
        return user;
    }

    public void VerifyEmail(string username)
    {
        const string query = "Update Users SET IsEmailVerified = @Verified WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Verified", true);
        parameters.Add("@Id", GetByUsername(username)!.Id);
        var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }
}