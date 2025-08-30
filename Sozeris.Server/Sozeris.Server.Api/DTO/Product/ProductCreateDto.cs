namespace Sozeris.Server.Api.DTO.Product;

public record ProductCreateDto
(
    string Name,
    decimal Price,
    string ImageBase64,
    bool isActive
);