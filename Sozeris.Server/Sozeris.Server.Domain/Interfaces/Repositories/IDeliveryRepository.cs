using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IDeliveryHistoryRepository
{
    Task<IReadOnlyList<DeliveryHistory>> GetAllDeliveryHistoryAsync(CancellationToken ct);
    Task<IReadOnlyList<DeliveryHistory>> GetDeliveryHistoryByDateAsync(DateTime date, CancellationToken ct);
    Task<DeliveryHistory?> GetDeliveryHistoryBySubscriptionAndDateAsync(int subscriptionId, DateTime date, CancellationToken ct);
    Task<DeliveryHistory> AddDeliveryHistoryAsync(DeliveryHistory deliveryHistory, CancellationToken ct);
    Task<DeliveryHistory> UpdateDeliveryHistoryAsync(DeliveryHistory deliveryHistory, CancellationToken ct);
}