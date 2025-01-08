using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var userFromDb = await _userService.GetUserByLoginAsync(user.Login);
    
        if (userFromDb == null)
        {
            return Unauthorized();
        }

        if (!_userService.VerifyPassword(user.Password, userFromDb.Password))
        {
            return Unauthorized();
        }

        var token = _authService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}