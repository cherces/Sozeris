using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.Delivery;

public record DeliveryForDayDto
(
    int SubscriptionId,
    string Address,
    List<DeliveryItemDto> Items,
    DeliveryStatus Status,
    string? Reason
);