using Business.Abstracts;
using Business.Constants;
using Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
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
        userService.ChangePassword(changePasswordDto);
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
}