using Sozeris.Models.Entities;

namespace Sozeris.Logic.Services.Interfaces;

public interface IUserService
{
    User GetCurrentUser();
    void UpdateUser(User user);
}