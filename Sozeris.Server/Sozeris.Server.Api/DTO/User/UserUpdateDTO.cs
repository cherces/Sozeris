using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.User;

public record UserUpdateDTO(
    int Id,
    string? Login,
    string? Password,
    UserRole? Role,
    string? Phone,
    string? Address
);