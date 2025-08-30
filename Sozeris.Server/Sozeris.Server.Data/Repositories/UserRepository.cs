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

    public async Task<IReadOnlyList<User>> GetAllUsersAsync(CancellationToken ct)
    {
        var users = await _context.Users.ToListAsync(ct);
        
        return users;
    }

    public async Task<User?> GetUserByIdAsync(int userId, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);
        
        return user;
    }

    public async Task<User?> GetUserByLoginAsync(string login, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login, ct);
        
        return user;
    }

    public async Task<User> AddUserAsync(User user, CancellationToken ct)
    {
        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
        return user;
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken ct)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(ct);
        return user;
    }

    public async Task DeleteUserByIdAsync(User user, CancellationToken ct)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(ct);
    }
}