using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories;

public class UserRepositories : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepositories(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(User userFilter)
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user;
    }
}