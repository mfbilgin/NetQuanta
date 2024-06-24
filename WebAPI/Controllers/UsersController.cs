using Business.Abstracts;
using Business.Constants.Messages;
using Core.Entities.Dtos.User;
using Core.Mailing;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(IUserService userService,IMailService mailService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUserList([FromQuery] int index = 1, [FromQuery] int size = 10)
    {
        var users = userService.GetAll(index, size);
        return Ok(users);
    }
    
    [HttpGet("id/{id}")]
    public IActionResult GetUserById(Guid id)
    {
        var user = userService.GetById(id);
        if (user == null)
        {
            return NotFound(UserMessages.UserNotFound);
        }

        return Ok(user);
    }
    
    [HttpGet("username/{username}")]
    public IActionResult GetUserByUsername(string username)
    {
        var user = userService.GetByUsername(username);
        if (user == null)
        {
            return NotFound(UserMessages.UserNotFound);
        }

        return Ok(user);
    }
    
    [HttpPut]
    public IActionResult UpdateUserInfos([FromBody] UserUpdateDto userUpdateDto)
    {
        userService.Update(userUpdateDto);
        return Ok(UserMessages.UserInfosHasBeenUpdated);
    }
    
    [HttpPut("change-password")]
    public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var user = userService.ChangePassword(changePasswordDto);
        mailService.SendPasswordChangedMail(user.Email, user.Username);
        return Ok(UserMessages.PasswordHasBeenChanged);
    }
    
    [HttpPut("change-role")]
    public IActionResult ChangeUserRole([FromBody] ChangeUserRoleDto changeUserRoleDto)
    {
        userService.ChangeUserRole(changeUserRoleDto);
        return Ok(UserMessages.RoleHasBeenChanged);
    }

    [HttpDelete]
    public IActionResult DeleteUser([FromBody] UserDeleteDto userDeleteDto)
    {
        userService.Delete(userDeleteDto);
        return Ok(UserMessages.UserHasBeenDeleted);
    }
    
    [HttpGet("remind-username/{email}")]
    public IActionResult RemindUsername(string email)
    {
        var username = userService.RemindUsername(email);
        mailService.SendUsernameReminderMail(email, username);
        return Ok(UserMessages.UsernameHasBeenSentToEmail);
    }
}