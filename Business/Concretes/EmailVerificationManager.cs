using Business.Abstracts;
using Business.BusinessRules;
using Core.Entities.Concretes;
using DataAccess.Abstracts;

namespace Business.Concretes;

public sealed class EmailVerificationManager(
    IEmailVerificationRepository emailVerificationRepository,
    EmailVerificationBusinessRules emailVerificationBusinessRules) : IEmailVerificationService
{
    public EmailVerification Add(string username)
    {
        emailVerificationBusinessRules.IfUserHasVerificationTokenDeleteFirst(username);
        
        var emailVerification = new EmailVerification
        {
            Id = Guid.NewGuid(),
            Username = username,
            Token = GenerateEmailVerificationToken(username)
        };
        emailVerificationRepository.Add(emailVerification);
        return emailVerification;
    }

    public void VerifyEmail(EmailVerification emailVerification)
    {
        emailVerification.Token = emailVerification.Token.Replace(' ', '+');
        emailVerification.Id = emailVerificationBusinessRules.TokenMustBeCorrectWhenEmailVerified(emailVerification.Username,
                emailVerification.Token).Id;
        emailVerificationRepository.Delete(emailVerification);
    }

    private static string GenerateEmailVerificationToken(string username)
    {
        var tokenBytes = new System.Security.Cryptography.HMACSHA512()
            .ComputeHash(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid() + username));
        var token = Convert.ToBase64String(tokenBytes)[..81];
        return token;
    }
}