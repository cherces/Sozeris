using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;

namespace Sozeris.Server.Data.Repositories;

public class JwtTokenRepository : IJwtTokenRepository
{
    private readonly ApplicationDbContext _context;

    public JwtTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddRefreshTokenAsync(JwtRefreshToken refreshToken)
    {
        await _context.JwtRefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }
    
    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.JwtRefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
        
        if (token is null) return;
        
        token.IsRevoked = true;
        
        await _context.SaveChangesAsync();
    }

    public async Task<JwtRefreshToken?> GetRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.JwtRefreshTokens
            .Include(u => u.User)
            .FirstOrDefaultAsync(t => t.Token == refreshToken && t.IsActive);
        
        return token;
    }
}