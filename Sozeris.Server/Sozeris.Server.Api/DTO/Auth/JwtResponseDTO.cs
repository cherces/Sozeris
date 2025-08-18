namespace Sozeris.Server.Api.DTO.Auth;

public record JwtResponseDTO
(
    string AccessToken,
    string? RefreshToken = null,
    string TokenType = "Bearer"
);