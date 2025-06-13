using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.Entities;
using Sozeris.Models.Enums;

namespace Sozeris.Logic.Services;

public class UserService : IUserService
{
    private User _user = new User
    {
        Id = 1,
        Login = "testuser",
        Address = "г. Москва, ул. Пушкина, д. 10"
    };

    public User GetCurrentUser() => _user;

    public void UpdateUser(User user)
    {
        _user = user;
    }
}