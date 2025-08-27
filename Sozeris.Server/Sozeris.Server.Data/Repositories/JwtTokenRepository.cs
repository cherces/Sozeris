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

    public async Task AddRefreshTokenAsync(JwtRefreshToken refreshToken, CancellationToken ct)
    {
        await _context.JwtRefreshTokens.AddAsync(refreshToken, ct);
        await _context.SaveChangesAsync(ct);
    }
    
    public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken ct)
    {
        var token = await _context.JwtRefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken, ct);
        
        if (token is null) return;
        
        token.IsRevoked = true;
        
        await _context.SaveChangesAsync(ct);
    }

    public async Task<JwtRefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken ct)
    {
        var token = await _context.JwtRefreshTokens
            .Include(u => u.User)
            .FirstOrDefaultAsync(t => t.Token == refreshToken && t.IsActive, ct);
        
        return token;
    }
}