using Sozeris.Server.Domain.Commons;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Interfaces.Services;

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

    public async Task<Result<User>> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return Result<User>.Fail("User not found");
        
        return Result<User>.Ok(user);
    }

    public async Task<Result<User>> GetUserByLoginAsync(string login)
    {
        var user = await _userRepository.GetUserByLoginAsync(login);
        if (user == null) return Result<User>.Fail("User not found");
        
        return Result<User>.Ok(user);
    }
    
    public async Task<Result<User>> CreateUserAsync(User user)
    {
        user.Password = PasswordHasher.HashPassword(user.Password);
        var newUser = await _userRepository.CreateUserAsync(user);
        
        return Result<User>.Ok(newUser);
    }

    public async Task<Result<User>> UpdateUserAsync(User user)
    {
        var oldUser = await _userRepository.GetUserByIdAsync(user.Id);
        
        if (oldUser == null) return Result<User>.Fail("User not found");
        
        oldUser.Password = PasswordHasher.HashPassword(user.Password);
        var updateUser = await _userRepository.UpdateUserAsync(oldUser);
        
        return Result<User>.Ok(updateUser);
    }

    public async Task<Result> DeleteUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return Result.Fail("User not found");
        
        await _userRepository.DeleteUserByIdAsync(user);
        return Result.Ok();
    }
}