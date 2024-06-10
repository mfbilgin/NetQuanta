using Core.Entities.Concretes;

namespace Business.Abstracts;

public interface IEmailVerificationService
{
    public EmailVerification Add(string username);
    public void VerifyEmail(EmailVerification emailVerification);
}