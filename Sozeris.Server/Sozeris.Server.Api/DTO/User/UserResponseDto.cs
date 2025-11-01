using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.User;

public record UserResponseDto
(
    int Id,
    string Login,
    UserRole Role,
    string Phone,
    string Address,
    bool IsActive
);