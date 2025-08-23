using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.Delivery;

public record DeliveryForDayDTO
(
    int SubscriptionId,
    string Address,
    List<DeliveryItemDTO> Items,
    DeliveryStatus Status,
    string? Reason
);