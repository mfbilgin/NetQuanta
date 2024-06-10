using Core.DataAccess;
using Core.Entities.Concretes;

namespace DataAccess.Abstracts;

public interface IUserRepository : IEntityRepository<User>
{
    public User? GetByUsername(string username);
    public User? GetByEmail(string email);
    public void VerifyEmail(string username);
}