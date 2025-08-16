namespace Sozeris.Server.Api.DTO.Order;

public record OrderCreateDTO
(
    int ProductId,
    int Quantity,
    decimal Price
);