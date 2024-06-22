using Core.DataAccess.EntityFramework;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public class EfPasswordResetTokenRepository(DbContext context) : EfEntityRepositoryBase<PasswordResetToken>(context), IPasswordResetTokenRepository
{
    public PasswordResetToken? GetByToken(string token)
    {
        return Get(passwordResetToken => passwordResetToken.Token == token);
    }

    public PasswordResetToken? GetByUsername(string username)
    {
        return Get(passwordResetToken => passwordResetToken.Username == username);
    }
}