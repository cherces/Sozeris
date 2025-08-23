using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;

namespace Sozeris.Server.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<User>> GetAllUsersAsync()
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

    public async Task<User> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserByIdAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}