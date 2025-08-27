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
    public async Task<ActionResult<ApiResponse<JwtResponseDTO>>> Login([FromBody] JwtRequestDTO jwtDto, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(jwtDto.Login, jwtDto.Password, ct);

        return result.Match(
            onSuccess: created => _mapper.Map<JwtResponseDTO>(created).ToApiResponse(this),
            onFailure: error => error.ToApiResponse<JwtResponseDTO>(this)
        );
    }
    
    [HttpPost("logout")]
    public async Task<ActionResult<ApiResponse>> Logout([FromBody] string refreshToken, CancellationToken ct)
    {
        var result = await _authService.LogoutAsync(refreshToken, ct);

        return result.Match(
            onSuccess: () => this.ToApiResponse(),
            onFailure: error => error.ToApiResponse(this)
        );
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult<ApiResponse<JwtResponseDTO>>> RefreshToken([FromBody] JwtRefreshDTO jwtRefreshDto, CancellationToken ct)
    {
        var result = await _authService.RefreshTokenAsync(jwtRefreshDto.RefreshToken, ct);

        return result.Match(
            onSuccess: refresh => _mapper.Map<JwtResponseDTO>(refresh).ToApiResponse(this),
            onFailure: error => error.ToApiResponse<JwtResponseDTO>(this)
        );
    }
}