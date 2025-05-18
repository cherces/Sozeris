using Sozeris.Models;
using Sozeris.Models.Commons;

namespace Sozeris.Logic.Services.Interfaces;

public interface IAuthService
{
    Task<JwtTokenModel> LoginAsync(LoginModel loginModel);
}