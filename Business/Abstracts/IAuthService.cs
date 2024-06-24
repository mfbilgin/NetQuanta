using Core.Entities.Concretes;
using Core.Entities.Dtos.Auth;
using Core.Security.Authorization;

namespace Business.Abstracts;

public interface IAuthService
{
    public User Register(RegisterDto registerDto);
    public User Login(LoginDto loginDto);
    public AccessToken CreateAccessToken(User user); 
}