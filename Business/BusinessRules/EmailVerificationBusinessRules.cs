using Business.Constants;
using Core.Entities.Concretes;
using Core.Exceptions;
using DataAccess.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessRules;

public class EmailVerificationBusinessRules(IEmailVerificationRepository emailVerificationRepository)
{
        
    public EmailVerification TokenMustBeCorrectWhenEmailVerified(string username, string token)
    {
        var emailVerification = emailVerificationRepository.GetByToken(token);
        if (emailVerification is null || emailVerification.Username != username)
        {
            throw new BusinessException(EmailVerificationMessages.EmailVerificationTokenIsIncorrect, StatusCodes.Status400BadRequest);
        }

        return emailVerification;
    }   
}