using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Auth;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, Mapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<JwtResponseDto>>> Login([FromBody] JwtRequestDto jwtDto, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(jwtDto.Login, jwtDto.Password, ct);
        
        if (result.IsSuccess)
            return _mapper.Map<JwtResponseDto>(result.Value).ToApiResponse();

        return result.Error.ToApiResponse<JwtResponseDto>(HttpContext);
    }
    
    [HttpPost("logout")]
    public async Task<ActionResult<ApiResponse>> Logout([FromBody] string refreshToken, CancellationToken ct)
    {
        var result = await _authService.LogoutAsync(refreshToken, ct);

        if (result.IsSuccess)
            return ApiResponse.Ok();
        
        return result.Error.ToApiResponse(HttpContext);
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult<ApiResponse<JwtResponseDto>>> RefreshToken([FromBody] JwtRefreshDto jwtRefreshDto, CancellationToken ct)
    {
        var result = await _authService.RefreshTokenAsync(jwtRefreshDto.RefreshToken, ct);
        
        if (result.IsSuccess)
            return _mapper.Map<JwtResponseDto>(result.Value).ToApiResponse();
        
        return result.Error.ToApiResponse<JwtResponseDto>(HttpContext);
    }
}