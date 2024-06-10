using Business.Abstracts;
using Business.Constants.Messages;
using Core.Entities.Dtos.Role;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RolesController(IRoleService roleService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] int index = 1, [FromQuery] int size = 10)
    {
        var roles = roleService.GetAll(index, size);
        return Ok(roles);
    }
    

    [HttpGet("name/{name}")]
    public IActionResult GetByName(string name)
    {
        var role = roleService.GetByName(name);
        if (role == null)
        {
            return NotFound(RoleMessages.RoleNotFound);
        }

        return Ok(role);
    }

    [HttpPost]
    public IActionResult Add([FromBody] RoleAddDto roleAddDto)
    {
        roleService.Add(roleAddDto);
        return Created("", RoleMessages.RoleHasBeenAdded);
    }

    [HttpPut]
    public IActionResult Update([FromBody] RoleUpdateDto roleUpdateDto)
    {
        roleService.Update(roleUpdateDto);
        return Ok(RoleMessages.RoleHasBeenUpdated);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] RoleDeleteDto roleDeleteDto)
    {
        roleService.Delete(roleDeleteDto);
        return Ok(RoleMessages.RoleHasBeenDeleted);
    }
}