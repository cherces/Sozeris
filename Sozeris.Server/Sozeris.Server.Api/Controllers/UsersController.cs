using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.User;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<UserResponseDTO>>> GetUserById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);

        return result.Match(
            onSuccess: user => _mapper.Map<UserResponseDTO>(user).ToApiResponse(this),
            onFailure: error => error.ToApiResponse<UserResponseDTO>(this)
        );
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserResponseDTO>>> CreateUser([FromBody] UserCreateDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var result = await _userService.CreateUserAsync(user);

        return result.Match(
            onSuccess: created => _mapper.Map<UserResponseDTO>(created)
                .ToCreatedAt(this, nameof(GetUserById), new { id = user.Id }),
            onFailure: error => error.ToApiResponse<UserResponseDTO>(this)
        );
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateUser(int userId, [FromBody] UserUpdateDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Id = userId;
        var result = await _userService.UpdateUserAsync(user);

        return result.Match(
            onSuccess: () => this.ToApiResponse(),
            onFailure: error => error.ToApiResponse(this)
        );
    }

    [HttpDelete("{userId:int}")]
    public async Task<ActionResult<ApiResponse>> DeleteUserById(int userId)
    {
        var result = await _userService.DeleteUserByIdAsync(userId);

        return result.Match(
            onSuccess: () => this.ToApiResponse(),
            onFailure: error => error.ToApiResponse(this)
        );
    }
}