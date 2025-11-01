using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.Delivery;

public record DeliveryMarkRequestDto
(
    DeliveryStatus Status,
    string? Reason
);