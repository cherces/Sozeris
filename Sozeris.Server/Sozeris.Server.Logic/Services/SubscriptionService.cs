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

    public async Task<IReadOnlyList<Subscription>> GetAllSubscriptionsAsync(CancellationToken ct)
    {
        var subscriptions = await _subscriptionRepository.GetAllSubscriptionsAsync(ct);
        
        return subscriptions;
    }

    public async Task<Result<Subscription>> GetSubscriptionByIdAsync(int subscriptionId, CancellationToken ct)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId, ct);
        if (subscription == null) return Result<Subscription>.Fail(DomainError.NotFound("Subscription", subscriptionId));
        
        return Result<Subscription>.Ok(subscription);
    }

    public async Task<IReadOnlyList<Subscription>> GetSubscriptionsByUserIdAsync(int userId, CancellationToken ct)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptionsByUserIdAsync(userId, ct);
        
        return subscriptions;
    }

    public async Task<Result<Subscription>> AddSubscriptionAsync(Subscription subscription, CancellationToken ct)
    {
        await _subscriptionRepository.AddSubscriptionAsync(subscription, ct);
        
        return Result<Subscription>.Ok(subscription);
    }
}