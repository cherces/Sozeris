namespace Sozeris.Server.Api.DTO.Order;

public record OrderCreateDto
(
    int ProductId,
    int Quantity,
    decimal Price
);