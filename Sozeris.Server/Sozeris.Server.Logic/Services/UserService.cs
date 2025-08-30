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

    public async Task<IReadOnlyList<User>> GetAllUsersAsync(CancellationToken ct)
    {
        var users = await _userRepository.GetAllUsersAsync(ct);
        
        return users;
    }

    public async Task<Result<User>> GetUserByIdAsync(int userId, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByIdAsync(userId, ct);
        if (user == null) return Result<User>.Fail(DomainError.NotFound("User", userId));
        
        return Result<User>.Ok(user);
    }

    public async Task<Result<User>> GetUserByLoginAsync(string login, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByLoginAsync(login, ct);
        if (user == null) return Result<User>.Fail(DomainError.NotFound("User", login));
        
        return Result<User>.Ok(user);
    }
    
    public async Task<Result<User>> AddUserAsync(User user, CancellationToken ct)
    {
        user.Password = PasswordHasher.HashPassword(user.Password);
        var newUser = await _userRepository.AddUserAsync(user, ct);
        
        return Result<User>.Ok(newUser);
    }

    public async Task<Result<User>> UpdateUserAsync(User user, CancellationToken ct)
    {
        var oldUser = await _userRepository.GetUserByIdAsync(user.Id, ct);
        
        if (oldUser == null) return Result<User>.Fail(DomainError.NotFound("User", user.Id));
        
        oldUser.Password = PasswordHasher.HashPassword(user.Password);
        var updateUser = await _userRepository.UpdateUserAsync(oldUser, ct);
        
        return Result<User>.Ok(updateUser);
    }

    public async Task<Result> DeleteUserByIdAsync(int userId, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByIdAsync(userId, ct);
        if (user == null) return Result.Fail(DomainError.NotFound("User", userId));
        
        await _userRepository.DeleteUserByIdAsync(user, ct);
        return Result.Ok();
    }
}