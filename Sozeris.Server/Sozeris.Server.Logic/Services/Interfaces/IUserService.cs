

using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<User>> GetAllUsersAsync(User userFilter);
    public Task<User> GetUserByIdAsync(int userId);
}