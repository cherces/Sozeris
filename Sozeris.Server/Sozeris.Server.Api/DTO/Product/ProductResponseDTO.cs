namespace Sozeris.Server.Api.DTO.Product;

public record ProductResponseDTO
(
    int Id,
    string Name,
    decimal Price,
    string ImageBase64
);