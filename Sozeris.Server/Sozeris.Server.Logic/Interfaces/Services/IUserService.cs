using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Interfaces.Services;

public interface IUserService
{
    public Task<IReadOnlyList<User>> GetAllUsersAsync();
    public Task<Result<User>> GetUserByIdAsync(int userId);
    public Task<Result<User>> GetUserByLoginAsync(string login);
    public Task<Result<User>> CreateUserAsync(User user);
    public Task<Result<User>> UpdateUserAsync(User user);
    public Task<Result> DeleteUserByIdAsync(int userId);
}