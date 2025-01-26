using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Data.Repositories;
using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.Commons;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IJwtTokenRepository _jwtTokenRepository;

    public AuthController(IAuthService authService, IUserService userService, JwtTokenRepository jwtTokenRepository)
    {
        _authService = authService;
        _userService = userService;
        _jwtTokenRepository = jwtTokenRepository;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var userFromDb = await _userService.GetUserByLoginAsync(user.Login);
    
        if (userFromDb == null)
            return Unauthorized();

        if (!_userService.VerifyPassword(user.Password, userFromDb.Password))
            return Unauthorized();

        var accessToken = _authService.GenerateAccessTokenAsync(user);
        var refreshToken = await _authService.GenerateRefreshTokenAsync(user.Id);
        
        return Ok(new JwtTokenModel { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        int? userId = await _jwtTokenRepository.GetUserIdByRefreshTokenAsync(refreshToken);
        if (userId == null)
            return Unauthorized();
        
        var userFromDb = await _userService.GetUserByIdAsync(userId.Value);
        
        var newAccessToken = _authService.GenerateAccessTokenAsync(userFromDb);
        var newRefreshToken = await _authService.GenerateRefreshTokenAsync(userId.Value);

        return Ok(new JwtTokenModel { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
    }
}