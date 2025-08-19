using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Enums;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Domain.Interfaces.Services;
using Sozeris.Server.Models.Models;

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

    public async Task<IReadOnlyList<Delivery>> GetDeliveriesForDayAsync()
    {
        var today = DateTime.UtcNow.Date;

        var subscriptions = await _subscriptionRepository.GetAllSubscriptionsAsync();
        var deliveryHistories = await _deliveryHistoryRepository.GetByDateAsync(today);
        
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
    
    public async Task<DeliveryHistory> MarkDeliveryAsync(int subscriptionId, DeliveryStatus status, string? reason)
    {
        var today = DateTime.UtcNow.Date;

        var existing = await _deliveryHistoryRepository.GetBySubscriptionAndDateAsync(subscriptionId, today);

        if (existing != null)
        {
            existing.Status = status;
            existing.Reason = status == DeliveryStatus.NotDelivered ? reason : null;

            await _deliveryHistoryRepository.UpdateAsync(existing);
            return existing;
        }

        var history = new DeliveryHistory
        {
            SubscriptionId = subscriptionId,
            DeliveryDate = today,
            Status = status,
            Reason = status == DeliveryStatus.NotDelivered ? reason : null
        };

        await _deliveryHistoryRepository.AddAsync(history);
        return history;
    }
}