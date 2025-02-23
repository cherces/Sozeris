using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services.Interfaces;

public interface ISubscriptionService
{    
    Task<IEnumerable<SubscriptionDTO>> GetAllSubscriptionsAsync();
    Task<SubscriptionDTO?> GetSubscriptionByIdAsync(int subscriptionId);
    Task<IEnumerable<SubscriptionDTO>> GetSubscriptionsByUserIdAsync(int userId);
    Task <SubscriptionDTO?> GetActiveSubscriptionByUserIdAsync(int userId);
    Task<bool> RenewSubscriptionByIdAsync(int subscriptionId);
    Task<bool> AddSubscriptionAsync(SubscriptionDTO subscription);
    Task<bool> DeleteSubscriptionByIdAsync(int subscriptionId);
}