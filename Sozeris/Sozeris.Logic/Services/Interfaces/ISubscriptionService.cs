using Sozeris.Models.Entities;

namespace Sozeris.Logic.Services.Interfaces;

public interface ISubscriptionService
{
    Task<List<Subscription>> GetSubscriptionsByUserIdAsync(int userId);
}