using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(User userFilter)
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user;
    }

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        return user;
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var oldUser = await _context.Users.FindAsync(user.Id);
        if (oldUser == null)
            return false;
        _context.Users.Update(oldUser);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteUserAsync(User user)
    {
        var userToDelete = await _context.Users.FindAsync(user.Id);
        if (userToDelete == null)
            return false;
        _context.Users.Remove(userToDelete);
        return await _context.SaveChangesAsync() > 0;
    }
}