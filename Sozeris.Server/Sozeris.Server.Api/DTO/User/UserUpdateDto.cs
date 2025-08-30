using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.DTO.User;

public record UserUpdateDto(
    int? Id,
    string Login,
    string Phone,
    string Address,
    bool IsActive
);