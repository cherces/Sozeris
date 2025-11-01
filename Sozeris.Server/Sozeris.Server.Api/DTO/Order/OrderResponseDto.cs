namespace Sozeris.Server.Api.DTO.Order;

public record OrderResponseDto
(
    int Id,
    int ProductId,
    string ProductName,
    int Quantity,
    decimal Price
);