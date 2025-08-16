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

    public async Task<bool> AddRefreshTokenAsync(int userId, string refreshToken)
    {
        var token = await _context.JwtRefreshTokens.FirstOrDefaultAsync(t => t.UserId == userId);

        if (token != null)
        {
            token.Token = refreshToken;
        }
        else
        {
            token = new JwtRefreshToken() { UserId = userId, Token = refreshToken };
            await _context.JwtRefreshTokens.AddAsync(token);
        }
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> DeleteRefreshTokenByUserIdAsync(int userId)
    {
        var token = await _context.JwtRefreshTokens.FirstOrDefaultAsync(t => t.UserId == userId);

        if (token != null)
        {
            _context.JwtRefreshTokens.Remove(token);
        }
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> DeleteRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.JwtRefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);

        if (token != null)
        {
            _context.JwtRefreshTokens.Remove(token);
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<string?> GetRefreshTokenAsync(int userId)
    {
        var token = await _context.JwtRefreshTokens.FirstOrDefaultAsync(t => t.UserId == userId);

        if (token != null)
        {
            return token.Token;
        }
        return null;
    }

    public async Task<int?> GetUserIdByRefreshTokenAsync(string refreshToken)
    {
        var refreshTokenEntity = await _context.JwtRefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
        
        return refreshTokenEntity?.UserId;
    }
}