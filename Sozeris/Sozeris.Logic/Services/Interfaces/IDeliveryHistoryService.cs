using Sozeris.Models.DTO;

namespace Sozeris.Logic.Services.Interfaces;

public interface IDeliveryHistoryService
{
    Task<List<DeliveryItemDTO>> GetDeliveriesByUserIdAsync(int userId);
}