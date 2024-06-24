using Business.Constants.Messages;
using Core.Entities.Concretes;
using Core.Exceptions;
using DataAccess.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessRules;

public sealed class EmailVerificationBusinessRules(IEmailVerificationRepository emailVerificationRepository)
{
        
    public EmailVerification TokenMustBeCorrectWhenEmailVerified(string username, string token)
    {
        var emailVerification = emailVerificationRepository.GetByToken(token);
        if (emailVerification is null || emailVerification.Username != username)
        {
            throw new BusinessException(EmailVerificationMessages.EmailVerificationTokenIsIncorrect, StatusCodes.Status400BadRequest,username);
        }

        return emailVerification;
    }

    public void IfUserHasVerificationTokenDeleteFirst(string username)
    {
        var emailVerification = emailVerificationRepository.GetByUsername(username);
        if (emailVerification is not null)
        {
            emailVerificationRepository.Delete(emailVerification);
        }
    }
    
    public void VerificationTokenCanNotBeResentInLessThanTwoMinute(string username)
    {
        var emailVerification = emailVerificationRepository.GetByUsername(username);
        if (emailVerification != null && emailVerification.CreatedAt < DateTime.Now.AddMinutes(-2))
        {
            throw new BusinessException(EmailVerificationMessages.EmailVerificationTokenCanNotBeResentInLessThanTwoMinute, StatusCodes.Status400BadRequest,username);
        }
    }
}