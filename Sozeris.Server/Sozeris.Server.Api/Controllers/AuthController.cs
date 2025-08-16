using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Data.Repositories;
using Sozeris.Server.Domain.Commons;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Domain.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IJwtTokenRepository _jwtTokenRepository;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IUserService userService, IJwtTokenRepository jwtTokenRepository, IMapper mapper)
    {
        _authService = authService;
        _userService = userService;
        _jwtTokenRepository = jwtTokenRepository;
        _mapper = mapper;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var userFromDb = await _userService.GetUserByLoginAsync(loginModel.Username);
    
        if (userFromDb == null)
            return Unauthorized();

        if (!_userService.VerifyPassword(loginModel.Password, userFromDb.Password))
            return Unauthorized();
        
        var accessToken = _authService.GenerateAccessTokenAsync(userFromDb);
        var refreshToken = await _authService.GenerateRefreshTokenAsync(userFromDb.Id);
        
        return Ok(new JwtTokenModel { AccessToken = accessToken, RefreshToken = refreshToken });
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken)
    {
        var userId = await _jwtTokenRepository.GetUserIdByRefreshTokenAsync(refreshToken);
        if (userId == null)
            return Unauthorized();

        await _jwtTokenRepository.DeleteRefreshTokenAsync(refreshToken);

        return Ok(new { message = "Logout successful" });
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