using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Interfaces.Services;

public interface ISubscriptionService
{    
    Task<IReadOnlyList<Subscription>> GetAllSubscriptionsAsync();
    Task<Result<Subscription>> GetSubscriptionByIdAsync(int subscriptionId);
    Task<IReadOnlyList<Subscription>> GetSubscriptionsByUserIdAsync(int userId);
    Task<Result<Subscription>> AddSubscriptionAsync(Subscription subscription);
}