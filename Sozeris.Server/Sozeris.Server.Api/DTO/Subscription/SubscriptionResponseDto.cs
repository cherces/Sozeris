using Sozeris.Server.Api.DTO.Order;

namespace Sozeris.Server.Api.DTO.Subscription;

public record SubscriptionResponseDto
(
    int Id,
    int UserId,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive,
    List<OrderResponseDto> Orders
);