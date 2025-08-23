using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IDeliveryHistoryRepository
{
    Task<IReadOnlyList<DeliveryHistory>> GetAllDeliveryHistoryAsync();
    Task<IReadOnlyList<DeliveryHistory>> GetDeliveryHistoryByDateAsync(DateTime date);
    Task<DeliveryHistory?> GetDeliveryHistoryBySubscriptionAndDateAsync(int subscriptionId, DateTime date);
    Task<DeliveryHistory> AddDeliveryHistoryAsync(DeliveryHistory deliveryHistory);
    Task<DeliveryHistory> UpdateDeliveryHistoryAsync(DeliveryHistory deliveryHistory);
}