using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules;
using Core.Entities.Concretes;
using Core.Entities.Dtos.Auth;
using Core.Security.Authorization;
using Core.Security.Hashing;

namespace Business.Concretes;

public sealed class AuthManager(
    AuthBusinessRules authBusinessRules,
    IRoleService roleService,
    ITokenHelper tokenHelper,
    IMapper mapper) : IAuthService
{
    public User Register(RegisterDto registerDto)
    {
        authBusinessRules.UsernameCanNotBeDuplicatedWhenRegistered(registerDto.Username);
        authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(registerDto.Email);

        HashingHelper.CreatePasswordHash(registerDto.Password, out var passwordHash, out var passwordSalt);
        var user = mapper.Map<User>(registerDto);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        return user;
    }

    public User Login(LoginDto loginDto)
    {
        var user = authBusinessRules.UsernameMustBeExistWhenRequested(loginDto.Username);
        authBusinessRules.PasswordMustBeCorrectWhenLoggedIn(user, loginDto.Password);
        authBusinessRules.EmailMustBeVerifiedWhenLoggedIn(user);
        return user;
    }
    
    public AccessToken CreateAccessToken(User user)
    {
        var role = mapper.Map<Role>(roleService.GetById(user.RoleId)!);
        var token = tokenHelper.CreateToken(user, role);
        return token;
    }

    public User ForgotPassword(ForgetPasswordDto forgetPasswordDto)
    {
        var user = authBusinessRules.UsernameMustBeExistWhenRequested(forgetPasswordDto.Username);
        return user;
    }
}