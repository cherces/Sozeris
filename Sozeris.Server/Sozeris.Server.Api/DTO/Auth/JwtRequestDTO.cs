namespace Sozeris.Server.Api.DTO.Auth;

public record JwtRequestDTO
(
    string Login,
    string Password,
    string? Device = null
);