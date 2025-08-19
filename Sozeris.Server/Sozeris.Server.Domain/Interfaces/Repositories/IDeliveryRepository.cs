using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IDeliveryHistoryRepository
{
    Task<IReadOnlyList<DeliveryHistory>> GetAllAsync();
    Task<IReadOnlyList<DeliveryHistory>> GetByDateAsync(DateTime date);
    Task<DeliveryHistory?> GetBySubscriptionAndDateAsync(int subscriptionId, DateTime date);
    Task<DeliveryHistory> AddAsync(DeliveryHistory deliveryHistory);
    Task<DeliveryHistory> UpdateAsync(DeliveryHistory deliveryHistory);
}