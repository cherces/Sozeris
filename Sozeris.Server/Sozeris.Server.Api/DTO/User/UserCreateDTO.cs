using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.User;

public record UserCreateDTO(
    string Login,
    string Password,
    UserRole Role,
    string Phone,
    string Address
);