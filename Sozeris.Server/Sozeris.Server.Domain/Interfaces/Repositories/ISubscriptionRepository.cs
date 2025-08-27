using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface ISubscriptionRepository
{
    Task<IReadOnlyList<Subscription>> GetAllSubscriptionsAsync(CancellationToken ct);
    Task<Subscription?> GetSubscriptionByIdAsync(int subscriptionId, CancellationToken ct);
    Task<IReadOnlyList<Subscription>> GetSubscriptionsByUserIdAsync(int userId, CancellationToken ct);
    Task<Subscription> AddSubscriptionAsync(Subscription subscription, CancellationToken ct);
}