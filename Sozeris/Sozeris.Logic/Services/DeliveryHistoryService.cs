using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.DTO;

namespace Sozeris.Logic.Services;

public class DeliveryHistoryService : IDeliveryHistoryService
{
    public async Task<List<DeliveryItemDTO>> GetDeliveriesByUserIdAsync(int userId)
    {
        await Task.Delay(100);

        return new List<DeliveryItemDTO>
        {
            new DeliveryItemDTO { Id = 1235, SubscriptionId = 345634, DeliveryDate = DateTime.UtcNow.AddDays(-10) },
            new DeliveryItemDTO { Id = 2246, SubscriptionId = 227543, DeliveryDate = DateTime.UtcNow.AddDays(-30) },
            new DeliveryItemDTO { Id = 3486, SubscriptionId = 646325, DeliveryDate = DateTime.UtcNow.AddDays(-60) }
        };
    }
}