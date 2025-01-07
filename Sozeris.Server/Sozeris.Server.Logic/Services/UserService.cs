using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(User userFilter)
    {
        var users = await _userRepository.GetAllUsersAsync(userFilter);
        return users;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user;
    }
}