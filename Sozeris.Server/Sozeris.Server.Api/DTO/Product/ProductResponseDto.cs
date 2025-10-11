namespace Sozeris.Server.Api.DTO.Product;

public record ProductResponseDto
(
    int Id,
    string Name,
    decimal Price,
    string ImageBase64,
    bool IsActive,
    string Description
);