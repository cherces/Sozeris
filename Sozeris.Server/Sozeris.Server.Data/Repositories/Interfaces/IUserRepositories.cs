using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories.Interfaces;

public interface IUserRepositories
{
    public Task<IEnumerable<User>> GetAllUsersAsync(User userFilter);
    public Task<User> GetUserByIdAsync(int userId);
}