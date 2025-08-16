using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO;
using Sozeris.Server.Api.DTO.User;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        
        return Ok(_mapper.Map<List<UserResponseDTO>>(users));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDTO>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        
        return user is null ? NotFound() : Ok(_mapper.Map<UserResponseDTO>(user));
    }

    [HttpPost]
    public async Task<ActionResult<UserCreateDTO>> CreateUser([FromBody] UserCreateDTO userDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var user = _mapper.Map<User>(userDto);
        var success = await _userService.CreateUserAsync(user);
        
        if (!success) return BadRequest("User creation failed.");

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDTO userDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var user = _mapper.Map<User>(userDto);
        var success = await _userService.UpdateUserAsync(user);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUserById(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        
        if (user == null) return NotFound();

        var success = await _userService.DeleteUserByIdAsync(userId);
        
        if (!success) return BadRequest("User deletion failed.");

        return NoContent();
    }
}