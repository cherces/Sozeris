using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Interfaces.Services;

namespace Sozeris.Server.Logic.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IReadOnlyList<Subscription>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.GetAllSubscriptionsAsync();
        
        return subscriptions;
    }

    public async Task<Result<Subscription>> GetSubscriptionByIdAsync(int subscriptionId)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId);
        if (subscription == null) return Result<Subscription>.Fail(DomainError.NotFound("Subscription", subscriptionId));
        
        return Result<Subscription>.Ok(subscription);
    }

    public async Task<IReadOnlyList<Subscription>> GetSubscriptionsByUserIdAsync(int userId)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptionsByUserIdAsync(userId);
        
        return subscriptions;
    }

    public async Task<Result<Subscription>> AddSubscriptionAsync(Subscription subscription)
    {
        await _subscriptionRepository.AddSubscriptionAsync(subscription);
        
        return Result<Subscription>.Ok(subscription);
    }
}