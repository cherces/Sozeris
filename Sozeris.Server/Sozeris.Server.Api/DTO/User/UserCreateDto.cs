using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.User;

public record UserCreateDto(
    string Login,
    string Password,
    UserRole Role,
    string Phone,
    string Address,
    bool? IsActive = true
);