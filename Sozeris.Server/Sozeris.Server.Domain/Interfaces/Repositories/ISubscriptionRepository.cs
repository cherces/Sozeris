using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface ISubscriptionRepository
{
    Task<IReadOnlyList<Subscription>> GetAllSubscriptionsAsync();
    Task<Subscription?> GetSubscriptionByIdAsync(int subscriptionId);
    Task<IReadOnlyList<Subscription>> GetSubscriptionsByUserIdAsync(int userId);
    Task<Subscription> AddSubscriptionAsync(Subscription subscription);
}