using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyList<User>> GetAllUsersAsync(CancellationToken ct);
    Task<User?> GetUserByIdAsync(int userId, CancellationToken ct);
    Task<User?> GetUserByLoginAsync(string login, CancellationToken ct);
    Task<User> AddUserAsync(User user, CancellationToken ct);
    Task<User> UpdateUserAsync(User user, CancellationToken ct);
    Task DeleteUserByIdAsync(User user, CancellationToken ct);
}