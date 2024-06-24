using Business.Constants.Messages;
using Core.Entities.Concretes;
using Core.Exceptions;
using DataAccess.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessRules;

public class PasswordResetTokenBusinessRules(
    IPasswordResetTokenRepository passwordResetTokenRepository)
{
    public PasswordResetToken TokenMustBeCorrectWhenRequested(string username, string token)
    {
        var passwordResetToken = passwordResetTokenRepository.GetByToken(token);
        if (passwordResetToken is null || passwordResetToken.Username != username)
        {
            throw new BusinessException(PasswordResetTokenMessages.PasswordResetTokenIsIncorrect, StatusCodes.Status400BadRequest,username);
        }

        return passwordResetToken;
    }
    public void IfUserHasPasswordResetTokenDeleteFirst(string username)
    {
        var passwordResetToken = passwordResetTokenRepository.GetByUsername(username);
        if (passwordResetToken is not null)
        {
            passwordResetTokenRepository.Delete(passwordResetToken);
        }
    }
    
    public void PasswordResetTokenCanNotBeResentInLessThanTwoMinute(string username)
    {
        var passwordResetToken = passwordResetTokenRepository.GetByUsername(username);
        if (passwordResetToken != null && passwordResetToken.CreatedAt < DateTime.Now.AddMinutes(-2))
        {
            throw new BusinessException(PasswordResetTokenMessages.PasswordResetTokenCanNotBeResentInLessThanTwoMinute, StatusCodes.Status400BadRequest,username);
        }
    }

    public PasswordResetToken TokenMustBeExistWhenDeleted(string token)
    {
        var passwordResetToken = passwordResetTokenRepository.GetByToken(token);
        if (passwordResetToken is null)
        {
            throw new BusinessException(PasswordResetTokenMessages.PasswordResetTokenIsNotFound, StatusCodes.Status400BadRequest,token);
        }

        return passwordResetToken;
    }
}