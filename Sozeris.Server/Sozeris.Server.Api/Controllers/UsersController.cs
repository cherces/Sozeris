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
    
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<UserResponseDto>>>> GetAllUsers(CancellationToken ct)
    {
        var users = await _userService.GetAllUsersAsync(ct);
        var dto  = _mapper.Map<List<UserResponseDto>>(users);
        
        return Ok(ApiResponse<List<UserResponseDto>>.Ok(dto));
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetUserById(int userId, CancellationToken ct)
    {
        var result = await _userService.GetUserByIdAsync(userId, ct);
        
        if (result.IsSuccess)
            return _mapper.Map<UserResponseDto>(result.Value).ToApiResponse();

        return result.Error.ToApiResponse<UserResponseDto>(HttpContext);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> AddUser([FromBody] UserCreateDto userDto, CancellationToken ct)
    {
        var user = _mapper.Map<User>(userDto);
        var result = await _userService.AddUserAsync(user, ct);
        
        if (result.IsSuccess)
            return _mapper.Map<UserResponseDto>(result.Value).ToApiResponse();
        
        return result.Error.ToApiResponse<UserResponseDto>(HttpContext);
    }

    [HttpPut("{userId:int}")]
    public async Task<ActionResult<ApiResponse>> UpdateUser(int userId, [FromBody] UserUpdateDto userDto, CancellationToken ct)
    {
        var user = _mapper.Map<User>(userDto);
        user.Id = userId;
        var result = await _userService.UpdateUserAsync(user, ct);
        
        if (result.IsSuccess)
            return Ok(ApiResponse.Ok());
        
        return result.Error.ToApiResponse(HttpContext);
    }

    [HttpDelete("{userId:int}")]
    public async Task<ActionResult<ApiResponse>> DeleteUserById(int userId, CancellationToken ct)
    {
        var result = await _userService.DeleteUserByIdAsync(userId, ct);
        
        if (result.IsSuccess)
            return Ok(ApiResponse.Ok());
        
        return result.Error.ToApiResponse(HttpContext);
    }
}