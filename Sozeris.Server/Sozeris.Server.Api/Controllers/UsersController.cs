using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Api.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserDTO? user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        var success = await _userService.CreateUserAsync(user);
        if (!success)
        {
            return BadRequest("User creation failed.");
        }

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserDTO? user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        var success = await _userService.UpdateUserAsync(user);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var success = await _userService.DeleteUserAsync(id);
        if (!success)
        {
            return BadRequest("User deletion failed.");
        }

        return NoContent();
    }
}