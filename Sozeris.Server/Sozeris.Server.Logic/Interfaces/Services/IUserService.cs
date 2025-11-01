using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Interfaces.Services;

public interface IUserService
{
    public Task<IReadOnlyList<User>> GetAllUsersAsync(CancellationToken ct);
    public Task<Result<User>> GetUserByIdAsync(int userId, CancellationToken ct);
    public Task<Result<User>> GetUserByLoginAsync(string login, CancellationToken ct);
    public Task<Result<User>> AddUserAsync(User user, CancellationToken ct);
    public Task<Result<User>> UpdateUserAsync(User user, CancellationToken ct);
    public Task<Result> DeleteUserByIdAsync(int userId, CancellationToken ct);
}