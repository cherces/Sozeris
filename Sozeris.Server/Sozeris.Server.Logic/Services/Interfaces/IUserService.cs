using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(int userId);
    public Task<User?> GetUserByLoginAsync(string login);
    public Task<bool> CreateUserAsync(UserDTO user);
    public Task<bool> UpdateUserAsync(UserDTO user);
    public Task<bool> DeleteUserByIdAsync(int userId);
    public string HashPassword(string password);
    public bool VerifyPassword(string password, string passwordHash);
}