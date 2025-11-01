namespace Sozeris.Server.Api.DTO.Product;

public record ProductUpdateDto
(
    int? Id,
    string Name,
    decimal Price,
    string ImageBase64,
    bool isActive
);