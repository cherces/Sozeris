using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Auth;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Logic.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<JwtResponseDTO>>> Login([FromBody] JwtRequestDTO jwtDto)
    {
        var result = await _authService.LoginAsync(jwtDto.Login, jwtDto.Password);
        if (!result.Success) return BadRequest(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));
        
        return Ok(ApiResponse<JwtResponseDTO>.Ok(
                new JwtResponseDTO(result.Value.AccessToken, result.Value.RefreshToken))
        );
    }
    
    [HttpPost("logout")]
    public async Task<ActionResult<ApiResponse>> Logout([FromBody] string refreshToken)
    {
        var result = await _authService.LogoutAsync(refreshToken);
        if (!result.Success) return BadRequest(ApiResponse.Fail(new Exception(result.ErrorMessage)));
        
        return Ok(ApiResponse.Ok());
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult<ApiResponse<JwtResponseDTO>>> RefreshToken([FromBody] JwtRefreshDTO jwtRefreshDto)
    {
        var result = await _authService.RefreshTokenAsync(jwtRefreshDto.RefreshToken);
        if (!result.Success) return BadRequest(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));
        
        return Ok(ApiResponse<JwtResponseDTO>.Ok(
            new JwtResponseDTO(result.Value.AccessToken, result.Value.RefreshToken))
        );
    }
}