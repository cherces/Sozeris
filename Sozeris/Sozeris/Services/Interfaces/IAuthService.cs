using Sozeris.Models;

namespace Sozeris.Services.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginModel loginModel);
}