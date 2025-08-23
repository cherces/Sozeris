using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.User;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Interfaces.Services;

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
    public async Task<ActionResult<ApiResponse<List<UserResponseDTO>>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        var dto  = _mapper.Map<List<UserResponseDTO>>(users);
        
        return Ok(ApiResponse<List<UserResponseDTO>>.Ok(dto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UserResponseDTO>>> GetUserById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        if (!result.Success) return NotFound(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));
        
        var dto = _mapper.Map<UserResponseDTO>(result.Value);
        
        return Ok(ApiResponse<UserResponseDTO>.Ok(dto));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserResponseDTO>>> CreateUser([FromBody] UserCreateDTO userDto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.Fail(ModelState));
        
        var user = _mapper.Map<User>(userDto);
        var result = await _userService.CreateUserAsync(user);
        if (!result.Success) return BadRequest(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));
        
        var response = _mapper.Map<UserResponseDTO>(result.Value);

        return CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateUser([FromBody] UserUpdateDTO userDto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.Fail(ModelState));
        
        var user = _mapper.Map<User>(userDto);
        var result = await _userService.UpdateUserAsync(user);
        if (!result.Success) return BadRequest(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));

        return Ok(ApiResponse.Ok());
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUserById(int userId)
    {
        var result = await _userService.DeleteUserByIdAsync(userId);
        if (!result.Success) return NotFound(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));

        return Ok(ApiResponse.Ok());
    }
}