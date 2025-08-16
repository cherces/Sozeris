using Sozeris.Server.Api.DTO.Order;

namespace Sozeris.Server.Api.DTO.Subscription;

public record SubscriptionCreateDTO
(
    int UserId,
    DateTime StartDate,
    DateTime EndDate,
    List<OrderCreateDTO> Orders
);