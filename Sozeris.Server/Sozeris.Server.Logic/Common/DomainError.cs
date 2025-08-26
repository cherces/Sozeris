using Sozeris.Server.Logic.Common.Enum;

namespace Sozeris.Server.Logic.Common;

public class DomainError
{
    public ErrorType Type { get; }
    public string Message { get; }

    private DomainError(ErrorType type, string message)
    {
        Type = type;
        Message = message;
    }

    public static DomainError NotFound(string entity, object key) =>
        new(ErrorType.NotFound, $"{entity} with key '{key}' was not found.");

    public static DomainError Validation(string message) =>
        new(ErrorType.Validation, message);

    public static DomainError Conflict(string message) =>
        new(ErrorType.Conflict, message);

    public static DomainError Unauthorized(string message = "Unauthorized") =>
        new(ErrorType.Unauthorized, message);

    public static DomainError Forbidden(string message = "Forbidden") =>
        new(ErrorType.Forbidden, message);

    public static DomainError Unexpected(string message) =>
        new(ErrorType.Unexpected, message);
}