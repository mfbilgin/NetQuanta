using Core.DataAccess;
using Core.Entities.Concretes;

namespace DataAccess.Abstracts;

public interface IPasswordResetTokenRepository : IEntityRepository<PasswordResetToken>
{
    public PasswordResetToken? GetByToken(string token);
    public PasswordResetToken? GetByUsername(string username);
}