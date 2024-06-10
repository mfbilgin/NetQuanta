using Core.DataAccess;
using Core.Entities.Concretes;

namespace DataAccess.Abstracts;

public interface IEmailVerificationRepository : IEntityRepository<EmailVerification>
{
    public EmailVerification? GetByToken(string token);
}