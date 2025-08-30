namespace Sozeris.Server.Api.DTO.Auth;

public record JwtResponseDto
(
    string AccessToken,
    string? RefreshToken = null,
    string TokenType = "Bearer"
);