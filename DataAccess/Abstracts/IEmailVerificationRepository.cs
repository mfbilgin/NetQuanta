using Core.DataAccess;
using Core.Entities.Concretes;

namespace DataAccess.Abstracts;

public interface IEmailVerificationRepository : IEntityRepository<EmailVerification>
{
    public EmailVerification? GetByToken(string token);
    public EmailVerification? GetByUsername(string username);
}