using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Enums;
using Sozeris.Server.Domain.Models;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Interfaces.Services;

public interface IDeliveryService
{
    Task<IReadOnlyList<Delivery>> GetDeliveriesForDayAsync();
    Task<Result<DeliveryHistory>> MarkDeliveryAsync(int subscriptionId, DeliveryStatus status, string? reason);
}