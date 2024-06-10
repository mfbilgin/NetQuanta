using System.Data;
using Core.Entities.Concretes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Contexts;

public class DapperDatabaseContext(IConfiguration configuration) : DbContext
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
    
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<EmailVerification> EmailVerifications { get; set; }
    
    
    public IDbConnection CreateConnection()=> new SqlConnection(_connectionString);
}