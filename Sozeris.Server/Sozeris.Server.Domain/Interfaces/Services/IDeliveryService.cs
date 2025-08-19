using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Enums;
using Sozeris.Server.Models.Models;

namespace Sozeris.Server.Domain.Interfaces.Services;

public interface IDeliveryService
{
    Task<IReadOnlyList<Delivery>> GetDeliveriesForDayAsync();
    Task<DeliveryHistory> MarkDeliveryAsync(int subscriptionId, DeliveryStatus status, string? reason);
}