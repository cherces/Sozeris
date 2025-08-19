using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;

namespace Sozeris.Server.Data.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Subscription>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _context.Subscriptions
            .Include(u => u.User)
            .Include(s=> s.Orders)
                .ThenInclude(o=>o.Product)
            .AsNoTracking()
            .ToListAsync();

        return subscriptions;
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(int subscriptionId)
    {
        var subscription = await _context.Subscriptions
            .Include(s => s.Orders)
            .ThenInclude(o => o.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == subscriptionId);
        
        return subscription;
    }

    public async Task<IReadOnlyList<Subscription>> GetSubscriptionsByUserIdAsync(int userId)
    {
        var subscriptions = await _context.Subscriptions
                .Include(s => s.Orders)
                .ThenInclude(o => o.Product)
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

        return subscriptions;
    }

    public async Task<Subscription> AddSubscriptionAsync(Subscription subscription)
    {
        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();
        return subscription;
    }
}