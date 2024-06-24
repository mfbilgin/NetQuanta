using Business.Abstracts;
using Business.BusinessRules;
using Core.Entities.Concretes;
using Core.Entities.Dtos.Auth;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class PasswordResetTokenManager(
    IPasswordResetTokenRepository resetTokenRepository,
    PasswordResetTokenBusinessRules resetTokenBusinessRules,
    UserBusinessRules userBusinessRules) : IPasswordResetTokenService
{
    public PasswordResetToken Add(ForgotPasswordDto forgotPasswordDto)
    {
        userBusinessRules.UsernameMustBeExist(forgotPasswordDto.Username);
        resetTokenBusinessRules.PasswordResetTokenCanNotBeResentInLessThanTwoMinute(forgotPasswordDto.Username);
        resetTokenBusinessRules.IfUserHasPasswordResetTokenDeleteFirst(forgotPasswordDto.Username);

        var passwordResetToken = new PasswordResetToken
        {
            Token = GenerateToken(),
            Username = forgotPasswordDto.Username
        };

        resetTokenRepository.Add(passwordResetToken);
        return passwordResetToken;
    }

    public void Delete(string token)
    {
        var passwordResetToken = resetTokenBusinessRules.TokenMustBeExistWhenDeleted(token);
        resetTokenRepository.Delete(passwordResetToken);
    }

    public PasswordResetToken? GetByToken(string token)
    {
        return resetTokenRepository.GetByToken(token);
    }

    public void ValidatePasswordResetToken(string token, string username)
    {
        var passwordResetToken = resetTokenBusinessRules.TokenMustBeCorrectWhenRequested(username, token);
        resetTokenRepository.Delete(passwordResetToken);
    }

    private static string GenerateToken()
    {
        var token = new Random().Next(100000, 999999).ToString();
        return token;
    }
}