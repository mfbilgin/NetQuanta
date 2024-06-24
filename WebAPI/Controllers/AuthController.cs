using Business.Abstracts;
using Business.Constants.Messages;
using Core.Entities.Concretes;
using Core.Entities.Dtos.Auth;
using Core.Mailing;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(
    IAuthService authService,
    IUserService userService,
    IEmailVerificationService emailVerificationService,
    IPasswordResetTokenService passwordResetTokenService,
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
    
    [HttpPost("resend-verification-email")]
    public IActionResult ResendVerificationEmail([FromBody] ResendVerificationEmailDto resendVerificationEmailDto)
    {
        var emailVerification = emailVerificationService.Add(resendVerificationEmailDto.Username);
        var email = userService.GetEmailByUsername(resendVerificationEmailDto.Username);
        mailService.SendWelcomeMail(email, emailVerification.Username,emailVerification.Token);
        return Ok(EmailVerificationMessages.EmailHasBeenSent);
    }
    
    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        var passwordResetToken = passwordResetTokenService.Add(forgotPasswordDto);
        var email = userService.GetEmailByUsername(forgotPasswordDto.Username);
        mailService.SendPasswordResetMail(email, passwordResetToken.Username,passwordResetToken.Token);
        return Ok(PasswordResetTokenMessages.PasswordResetTokenHasBeenSent);
    }
    
    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        passwordResetTokenService.ValidatePasswordResetToken(resetPasswordDto.Token, resetPasswordDto.Username);
        var user = userService.ResetPassword(resetPasswordDto);
        mailService.SendPasswordChangedMail(user.Email, user.Username);
        return Ok(PasswordResetTokenMessages.PasswordHasBeenReset);
    }

} 