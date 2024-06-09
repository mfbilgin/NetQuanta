using Business.Abstracts;
using Business.Constants;
using Core.Entities.Concretes;
using Core.Mailing;
using Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthService authService,
    IUserService userService,
    IRoleService roleService,
    IEmailVerificationService emailVerificationService,
    IMailService mailService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto registerDto)
    {
        var user = authService.Register(registerDto);
        userService.Add(user);
        var emailVerification = emailVerificationService.Add(user.Username);
        mailService.SendWelcomeMail(user.Email, emailVerification.Username,emailVerification.Token);
        return Ok(UserMessages.UserRegistered);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        var user = authService.Login(loginDto);
        var token = authService.CreateAccessToken(user);
        return Ok(token);
    }
    
    [HttpGet("verify-email")]
    public IActionResult VerifyEmail([FromQuery] string username, [FromQuery] string token)
    {
        var emailVerification = new EmailVerification
        {
            Token = token,
            Username = username
        };
        emailVerificationService.VerifyEmail(emailVerification);
        userService.VerifyEmail(username);
        return Ok(EmailVerificationMessages.EmailHasBeenVerified);
    }
}