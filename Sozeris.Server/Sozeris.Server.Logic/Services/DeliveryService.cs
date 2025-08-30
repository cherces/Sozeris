using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Enums;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Logic.Interfaces.Services;
using Sozeris.Server.Domain.Models;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Services;

public class DeliveryService : IDeliveryService
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IDeliveryHistoryRepository _deliveryHistoryRepository;

    public DeliveryService(ISubscriptionRepository subscriptionRepository, IDeliveryHistoryRepository deliveryHistoryRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _deliveryHistoryRepository = deliveryHistoryRepository;
    }

    public async Task<IReadOnlyList<Delivery>> GetDeliveriesTodayAsync(CancellationToken ct)
    {
        var today = DateTime.UtcNow.Date;

        var subscriptions = await _subscriptionRepository.GetAllSubscriptionsAsync(ct);
        var deliveryHistories = await _deliveryHistoryRepository.GetDeliveryHistoryByDateAsync(today, ct);
        
        var deliveries = subscriptions
            .Where(s => s.IsActive && s.StartDate <= today && s.EndDate >= today)
            .Select(s =>
            {
                var history = deliveryHistories.FirstOrDefault(h => h.SubscriptionId == s.Id);

                return new Delivery
                {
                    SubscriptionId = s.Id,
                    Address = s.User.Address,
                    Date = today,
                    Status = history?.Status ?? DeliveryStatus.Pending,
                    Reason = history?.Reason ?? "Обратитесь в поддержку",
                    Items = s.Orders.Select(o => new DeliveryItem
                    {
                        ProductName = o.Product.Name,
                        Quantity = o.Quantity
                    }).ToList()
                };
            }).ToList();

        return deliveries;
    }
    
    public async Task<Result<DeliveryHistory>> MarkDeliveryAsync(int subscriptionId, DeliveryStatus status, string? reason, CancellationToken ct)
    {
        var today = DateTime.UtcNow.Date;

        var existing = await _deliveryHistoryRepository.GetDeliveryHistoryBySubscriptionAndDateAsync(subscriptionId, today, ct);

        if (existing != null)
        {
            existing.Status = status;
            existing.Reason = status == DeliveryStatus.NotDelivered ? reason : null;

            await _deliveryHistoryRepository.UpdateDeliveryHistoryAsync(existing, ct);
            return Result<DeliveryHistory>.Ok(existing);
        }

        var history = new DeliveryHistory
        {
            SubscriptionId = subscriptionId,
            DeliveryDate = today,
            Status = status,
            Reason = status == DeliveryStatus.NotDelivered ? reason : null
        };

        await _deliveryHistoryRepository.AddDeliveryHistoryAsync(history, ct);
        
        return Result<DeliveryHistory>.Ok(history);
    }
}