using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services.Interfaces;

public interface IAuthService
{
    public string GenerateToken(User user);
}