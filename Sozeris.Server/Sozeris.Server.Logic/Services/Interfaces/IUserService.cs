

using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<User>> GetAllUsersAsync(User userFilter);
    public Task<User?> GetUserByIdAsync(int userId);
    public Task<User?> GetUserByLoginAsync(string login);
    public Task<bool> CreateUserAsync(User user);
    public Task<bool> UpdateUserAsync(User user);
    public Task<bool> DeleteUserAsync(User user);
    public string HashPassword(string password);
    public bool VerifyPassword(string password, string passwordHash);
}