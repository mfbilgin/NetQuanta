using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules;
using Business.ValidationRules.FluentValidation.UserValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Security;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concretes;
using Core.Entities.Dtos.Auth;
using Core.Entities.Dtos.User;
using Core.Extensions.Paging;
using Core.Security.Hashing;
using DataAccess.Abstracts;

namespace Business.Concretes;

public sealed class UserManager(IUserRepository userRepository, UserBusinessRules userBusinessRules, IMapper mapper)
    : IUserService
{

    [CacheRemoveAspect("IUserService.Get")]
    public void Add(User user)
    {
        userBusinessRules.SetUserRole(user);
        user.Id = Guid.NewGuid();
        user.Username = user.Username.ToLower();
        userRepository.Add(user);
    }
    [SecurityAspect("all")]
    [ValidationAspect(typeof(UserUpdateValidator))]
    [CacheRemoveAspect("IUserService.Get")]
    public void Update(UserUpdateDto userUpdateDto)
    {
        userBusinessRules.UsersJustCanUpdateTheirOwnInformations(userUpdateDto.Id);
        userBusinessRules.UserIdMustBeExist(userUpdateDto.Id);
        userBusinessRules.UsernameCanNotBeDuplicatedWhenUpdated(userUpdateDto.Username, userUpdateDto.Id);
        userBusinessRules.EmailCanNotBeDuplicatedWhenUpdated(userUpdateDto.Email, userUpdateDto.Id);

        var user = mapper.Map<User>(userUpdateDto);
        userRepository.Update(user);
    }

    [SecurityAspect("all")]
    [CacheRemoveAspect("IUserService.Get")]
    public void Delete(UserDeleteDto userDeleteDto)
    {
        userBusinessRules.UsersJustCanDeleteTheirOwnInformations(userDeleteDto.Id);
        userBusinessRules.UserIdMustBeExist(userDeleteDto.Id);
        var user = mapper.Map<User>(userDeleteDto);
        userRepository.Delete(user);
    }
    
    
    [SecurityAspect("all")]
    public User ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var user = userBusinessRules.UserIdMustBeExist(changePasswordDto.Id);
        userBusinessRules.UsersJustCanUpdateTheirOwnInformations(changePasswordDto.Id);
        userBusinessRules.PasswordsCanNotBeSame(changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
        userBusinessRules.CurrentPasswordMustBeCorrect(user, changePasswordDto.CurrentPassword);
        HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out var passwordHash, out var passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        userRepository.Update(user);
        return user;
    }

    public User ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = userBusinessRules.UsernameMustBeExist(resetPasswordDto.Username);
        HashingHelper.CreatePasswordHash(resetPasswordDto.Password, out var passwordHash, out var passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        userRepository.Update(user);
        return user;
    }

    public string RemindUsername(string email)
    {
        var user = userBusinessRules.EmailMustBeExist(email);
        return user.Username;
    }

    [SecurityAspect("admin")]
    [CacheRemoveAspect("IUserService.Get")]
    public void ChangeUserRole(ChangeUserRoleDto changeUserRoleDto)
    {
        var user = userBusinessRules.UsernameMustBeExist(changeUserRoleDto.Username);
        var role = userBusinessRules.RoleMustBeExistWhenUserRoleUpdated(changeUserRoleDto.Role);
        user.RoleId = role.Id;
        userRepository.Update(user);
    }

    public string GetEmailByUsername(string username)
    {
        var user= userBusinessRules.UsernameMustBeExist(username);
        return user.Email;
    }

    public void VerifyEmail(string username)
    {
        userRepository.VerifyEmail(username);
    }

    [SecurityAspect("admin")]
    [CacheAspect]
    public PageableModel<UserGetDto> GetAll(int index = 1, int size = 10)
    {
        var users = userRepository.GetList(index, size);
        return mapper.Map<PageableModel<UserGetDto>>(users);
    }

    [SecurityAspect("admin")]
    [CacheAspect]
    public UserGetDto? GetById(Guid id)
    {
        return mapper.Map<UserGetDto?>(userRepository.GetById(id));
    }
    
    [SecurityAspect("all")]
    [CacheAspect]
    public UserGetDto? GetByUsername(string name)
    {
        userBusinessRules.UsersCannotAccessOtherUsersInformations(name);
        return mapper.Map<UserGetDto?>(userRepository.GetByUsername(name));
    }
}