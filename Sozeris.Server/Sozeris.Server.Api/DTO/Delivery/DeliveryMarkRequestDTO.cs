using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.Delivery;

public record DeliveryMarkRequestDTO
(
    DeliveryStatus Status,
    string? Reason
);