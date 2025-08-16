namespace Sozeris.Server.Api.DTO.Product;

public record ProductCreateDTO
(
    string Name,
    decimal Price,
    string ImageBase64
);