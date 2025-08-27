using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Interfaces.Services;

public interface ISubscriptionService
{    
    Task<IReadOnlyList<Subscription>> GetAllSubscriptionsAsync(CancellationToken ct);
    Task<Result<Subscription>> GetSubscriptionByIdAsync(int subscriptionId, CancellationToken ct);
    Task<IReadOnlyList<Subscription>> GetSubscriptionsByUserIdAsync(int userId, CancellationToken ct);
    Task<Result<Subscription>> AddSubscriptionAsync(Subscription subscription, CancellationToken ct);
}