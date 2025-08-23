using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.Delivery;

public record DeliveryForHistoryDTO
(
    int SubscriptionId,
    DateTime DeliveryDate,
    DeliveryStatus Status,
    string? Reason
);