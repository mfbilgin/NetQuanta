using Core.Entities.Concretes;
using Core.Entities.Dtos.Auth;

namespace Business.Abstracts;

public interface IPasswordResetTokenService
{
    public PasswordResetToken Add(ForgotPasswordDto forgotPasswordDto);
    public void Delete(string token);
    public PasswordResetToken? GetByToken(string token);
    public void ValidatePasswordResetToken(string token, string username);
}