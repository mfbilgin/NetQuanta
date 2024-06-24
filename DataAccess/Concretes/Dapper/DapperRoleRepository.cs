using System.Linq.Expressions;
using Core.Entities.Concretes;
using Core.Extensions.Paging;
using Dapper;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using DataAccess.Contexts;

namespace DataAccess.Concretes.Dapper;

public sealed class DapperRoleRepository(DapperDatabaseContext context) : IRoleRepository
{
    public void Add(Role entity)
    {
        const string query = "INSERT INTO Roles(Id,Name) VALUES(@Id, @Name)";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@Name", entity.Name.Trim());
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public void Update(Role entity)
    {
        const string query = "UPDATE Roles SET Name = @Name WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        parameters.Add("@Name", entity.Name.Trim());
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }

    public void Delete(Role entity)
    {
        const string query = "DELETE FROM Roles WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id);
        using var connection = context.CreateConnection();
        connection.Execute(query, parameters);
        connection.Close();
    }


    public Role? GetById(Guid id)
    {
        const string query = "SELECT * FROM Roles WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        using var connection = context.CreateConnection();
        var role = connection.QueryFirstOrDefault<Role>(query, parameters);
        connection.Close();
        return role;
    }

    public Role? GetByName(string name)
    {
        const string query = "SELECT * FROM Roles WHERE Name = @Name";
        var parameters = new DynamicParameters();
        parameters.Add("@Name", name.Trim());
        using var connection = context.CreateConnection();
        var role = connection.QueryFirstOrDefault<Role>(query, parameters);
        connection.Close();
        return role;
    }

    public Role? Get(Expression<Func<Role, bool>> filter)
    {
        throw new InvalidOperationException("Filtering is not supported in dapper repository.");
    }

    public PageableModel<Role> GetList(int index, int size, Expression<Func<Role, bool>>? filter = null)
    {
        if (filter != null)
        {
            throw new InvalidOperationException("Filtering is not supported in dapper repository.");
        }

        const string query = "SELECT * FROM Roles";
        using var connection = context.CreateConnection();
        var roles = connection.Query<Role>(query).ToList();
        connection.Close();
        return roles.ToPaginate(index,size);
    }
}