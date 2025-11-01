using Sozeris.Server.Api.DTO.Order;

namespace Sozeris.Server.Api.DTO.Subscription;

public record SubscriptionCreateDto
(
    int UserId,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive,
    List<OrderCreateDto> Orders
);