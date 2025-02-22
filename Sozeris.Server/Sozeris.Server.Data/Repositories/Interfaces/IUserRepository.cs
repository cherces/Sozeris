using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(int userId);
    public Task<User?> GetUserByLoginAsync(string login);
    public Task<bool> CreateUserAsync(User user);
    public Task<bool> UpdateUserAsync(User user);
    public Task<bool> DeleteUserAsync(int userId);
}