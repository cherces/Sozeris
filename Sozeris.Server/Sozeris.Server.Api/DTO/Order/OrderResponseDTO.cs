namespace Sozeris.Server.Api.DTO.Order;

public record OrderResponseDTO
(
    int Id,
    int ProductId,
    string ProductName,
    int Quantity,
    decimal Price
);