using Business.Constants;
using Core.Entities.Concretes;
using Core.Exceptions;
using Core.Security.Hashing;
using DataAccess.Abstracts;using Microsoft.AspNetCore.Http;

namespace Business.BusinessRules;

public sealed class AuthBusinessRules(IUserRepository userRepository)
{
    public void UsernameCanNotBeDuplicatedWhenRegistered(string username)
    {
        var user = userRepository.GetByUsername(username);
        if (user is not null)
        {
            throw new BusinessException(UserMessages.UsernameAlreadyExists, StatusCodes.Status409Conflict);
        }
    }
    public void EmailCanNotBeDuplicatedWhenRegistered(string email)
    {
        var user = userRepository.GetByEmail(email);
        if (user is not null)
        {
            throw new BusinessException(UserMessages.EmailAlreadyExists, StatusCodes.Status409Conflict);
        }
    }
    public User UsernameMustBeExistWhenRequested(string username)
    {
        var user = userRepository.GetByUsername(username);
        if (user is null)
        {
            throw new BusinessException(UserMessages.UserNotFound, StatusCodes.Status404NotFound);
        }

        return user;
    }
    public void PasswordMustBeCorrectWhenLoggedIn(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            throw new BusinessException(UserMessages.CurrentPasswordIsIncorrect, StatusCodes.Status400BadRequest);
        }
    }
    
    public void EmailMustBeVerifiedWhenLoggedIn(User user)
    {
        if (!user.IsEmailVerified)
        {
            throw new BusinessException(UserMessages.EmailNotVerified, StatusCodes.Status400BadRequest);
        }
    }

}