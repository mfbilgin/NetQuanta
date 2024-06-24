using Business.Constants.Messages;
using Core.Entities.Concretes;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Security.Authorization;
using Core.Security.Hashing;
using DataAccess.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessRules;

public sealed class UserBusinessRules(IUserRepository userRepository, IRoleRepository roleRepository)
{
    public void UsernameCanNotBeDuplicatedWhenUpdated(string username, Guid id)
    {
        var user = userRepository.GetByUsername(username);
        if (user is not null && user.Id != id)
        {
            throw new BusinessException(UserMessages.UsernameAlreadyExists, StatusCodes.Status409Conflict, username);
        }
    }

    public void SetUserRole(User user)
    {
        user.RoleId = roleRepository.GetByName(SystemRoles.User.ToString().ToLower())!.Id;
    }

    public void UsersJustCanUpdateTheirOwnInformations(Guid requestedUserId)
    {
        if (requestedUserId != JwtHelper.GetAuthenticatedUserId() &&
            JwtHelper.GetAuthenticatedUserRoles().Contains(SystemRoles.Admin.ToString().ToLower()) is false)
        {
            throw new AuthorizationException(null);
        }
    }

    public void UsersJustCanDeleteTheirOwnInformations(Guid id)
    {
        if (id != JwtHelper.GetAuthenticatedUserId() &&
            JwtHelper.GetAuthenticatedUserRoles().Contains(SystemRoles.Admin.ToString().ToLower()) is false)
        {
            throw new AuthorizationException(null);
        }
    }

    public void UsersCannotAccessOtherUsersInformations(string name)
    {
        if (name != JwtHelper.GetAuthenticatedUsername() &&
            JwtHelper.GetAuthenticatedUserRoles().Contains(SystemRoles.Admin.ToString().ToLower()) is false)
        {
            throw new AuthorizationException(name);
        }
    }

    public void EmailCanNotBeDuplicatedWhenUpdated(string email, Guid id)
    {
        var user = userRepository.GetByEmail(email);
        if (user is not null && user.Id != id)
        {
            throw new BusinessException(UserMessages.EmailAlreadyExists, StatusCodes.Status409Conflict, email);
        }
    }


    public User UserIdMustBeExist(Guid id)
    {
        var user = userRepository.GetById(id);
        if (user is null)
        {
            throw new BusinessException(UserMessages.UserNotFound, StatusCodes.Status404NotFound,id.ToString());
        }

        return user;
    }

    public User UsernameMustBeExist(string username)
    {
        var user = userRepository.GetByUsername(username);
        if (user is null)
        {
            throw new BusinessException(UserMessages.UserNotFound, StatusCodes.Status404NotFound,username);
        }

        return user;
    }

    public void CurrentPasswordMustBeCorrect(User user, string currentPassword)
    {
        if (HashingHelper.VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt) is false)
        {
            throw new BusinessException(UserMessages.CurrentPasswordIsIncorrect, StatusCodes.Status400BadRequest,user.Username);
        }
    }

    public void PasswordsCanNotBeSame(string currentPassword, string newPassword)
    {
        if (currentPassword == newPassword)
        {
            throw new BusinessException(UserMessages.NewPasswordMustBeDifferent, StatusCodes.Status400BadRequest,null);
        }
    }

    public Role RoleMustBeExistWhenUserRoleUpdated(string roleName)
    {
        var role = roleRepository.GetByName(roleName);
        if (role is null)
        {
            throw new BusinessException(RoleMessages.RoleNotFound, StatusCodes.Status404NotFound,roleName);
        }

        return role;
    }

    public void UserEmailCanNotBeVerified(string username)
    {
        var user = userRepository.GetByUsername(username);
        if (user is null)
        {
            throw new BusinessException(UserMessages.UserNotFound, StatusCodes.Status404NotFound,username);
        }
        if (user.IsEmailVerified)
        {
            throw new BusinessException(UserMessages.EmailAlreadyVerified, StatusCodes.Status400BadRequest,username);
        }
    }

    public User EmailMustBeExist(string email)
    {
        var user = userRepository.GetByEmail(email);
        if (user is null)
        {
            throw new BusinessException(UserMessages.UserNotFound, StatusCodes.Status404NotFound,email);
        }

        return user;
    }
}