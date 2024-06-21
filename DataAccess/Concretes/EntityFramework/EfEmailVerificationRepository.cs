using Core.DataAccess.EntityFramework;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework;

public sealed class EfEmailVerificationRepository(DbContext context) : EfEntityRepositoryBase<EmailVerification>(context), IEmailVerificationRepository
{
    public EmailVerification? GetByToken(string token)
    {
        return Get(email => email.Token == token);
    }

    public EmailVerification? GetByUsername(string username)
    {
        return Get(email => email.Username == username);
    }
}