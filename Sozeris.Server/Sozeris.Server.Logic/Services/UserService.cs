using Sozeris.Server.Domain.Commons;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Domain.Interfaces.Services;

namespace Sozeris.Server.Logic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyList<User>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        
        return users;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        
        return user;
    }

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        var user = await _userRepository.GetUserByLoginAsync(login);
        
        return user;
    }
    
    public async Task<bool> CreateUserAsync(User user)
    {
        user.Password = PasswordHasher.HashPassword(user.Password);
        
        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var oldUser = await _userRepository.GetUserByIdAsync(user.Id);
        
        if (oldUser == null) return false;
        
        oldUser.Password = PasswordHasher.HashPassword(user.Password);
        
        return await _userRepository.UpdateUserAsync(oldUser);
    }

    public async Task<bool> DeleteUserByIdAsync(int userId)
    {
        return await _userRepository.DeleteUserByIdAsync(userId);
    }
}