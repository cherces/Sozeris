using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _context.Subscriptions.ToListAsync();

        return subscriptions;
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(int subscriptionId)
    {
        var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
        
        return subscription;
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptionsByUserIdAsync(int userId)
    {
        var subscriptions = await _context.Subscriptions.Where(x => x.UserId == userId).ToListAsync();
        
        return subscriptions;
    }

    public async Task<Subscription?> GetActiveSubscriptionByUserIdAsync(int userId)
    {
        var subscriptions = await _context.Subscriptions
            .Where(x => x.UserId == userId && x.IsActive)
            .FirstOrDefaultAsync();

        return subscriptions;
    }

    public async Task<bool> RenewSubscriptionByIdAsync(int subscriptionId)
    {
        var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
        
        if (subscription == null) return false;

        //пока без логики
        subscription.IsActive = true;
        
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddSubscriptionAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
        
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteSubscriptionByIdAsync(int subscriptionId)
    {
        var subscriptionToDelete = await _context.Subscriptions.FindAsync(subscriptionId);
        
        if (subscriptionToDelete == null) return false;
        
        _context.Subscriptions.Remove(subscriptionToDelete);
        
        return await _context.SaveChangesAsync() > 0;
    }
}