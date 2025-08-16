namespace Sozeris.Server.Api.DTO.Product;

public record ProductUpdateDTO
(
    int Id,
    string? Name,
    decimal? Price,
    string? ImageBase64
);