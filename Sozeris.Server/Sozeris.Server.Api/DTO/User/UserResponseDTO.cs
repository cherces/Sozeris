using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.User;

public record UserResponseDTO
(
    int Id,
    string Login,
    UserRole Role,
    string Phone,
    string Address
);