using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories.Interfaces;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
    Task<Subscription?> GetSubscriptionByIdAsync(int subscriptionId);
    Task<IEnumerable<Subscription>> GetSubscriptionsByUserIdAsync(int userId);
    Task <Subscription?> GetActiveSubscriptionByUserIdAsync(int userId);
    Task<bool> RenewSubscriptionByIdAsync(int subscriptionId);
    Task<bool> AddSubscriptionAsync(Subscription subscription);
    Task<bool> DeleteSubscriptionByIdAsync(int subscriptionId);
}