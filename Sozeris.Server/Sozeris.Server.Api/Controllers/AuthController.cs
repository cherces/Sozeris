using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Auth;
using Sozeris.Server.Domain.Commons;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Domain.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<JwtResponseDTO>> Login([FromBody] JwtRequestDTO jwtDto)
    {
        var tokens = await _authService.LoginAsync(jwtDto.Login, jwtDto.Password);
        
        return Ok(new JwtResponseDTO(tokens.AccessToken, tokens.RefreshToken));
    }
    
    [HttpPost("logout")]
    public async Task<ActionResult> Logout([FromBody] string refreshToken)
    {
        await _authService.LogoutAsync(refreshToken);
        return Ok(new { message = "Logout successful" });
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult<JwtResponseDTO>> RefreshToken([FromBody] JwtRefreshDTO jwtRefreshDto)
    {
        var tokens = await _authService.RefreshTokenAsync(jwtRefreshDto.RefreshToken);
        
        return Ok(new JwtResponseDTO(tokens.AccessToken, tokens.RefreshToken));
    }
}