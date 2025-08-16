using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Services;

public interface IUserService
{
    public Task<IReadOnlyList<User>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(int userId);
    public Task<User?> GetUserByLoginAsync(string login);
    public Task<bool> CreateUserAsync(User user);
    public Task<bool> UpdateUserAsync(User user);
    public Task<bool> DeleteUserByIdAsync(int userId);
    public string HashPassword(string password);
    public bool VerifyPassword(string password, string passwordHash);
}