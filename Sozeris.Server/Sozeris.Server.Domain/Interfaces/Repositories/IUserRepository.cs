using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyList<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByLoginAsync(string login);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserByIdAsync(User user);
}