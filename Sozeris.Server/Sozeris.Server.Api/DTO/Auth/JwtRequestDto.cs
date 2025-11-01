namespace Sozeris.Server.Api.DTO.Auth;

public record JwtRequestDto
(
    string Login,
    string Password,
    string? Device = null
);