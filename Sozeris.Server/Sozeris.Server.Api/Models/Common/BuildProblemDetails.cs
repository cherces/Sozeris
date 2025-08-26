using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Common.Enum;

namespace Sozeris.Server.Api.Models.Common;

public static class BuildProblemDetails
{
    public static ProblemDetails Build(Exception ex, HttpContext? context = null) => 
        ex switch
        {
            UnauthorizedAccessException => Make(401, "Unauthorized", SafeMessage(ex), context),
            KeyNotFoundException => Make(404, "Not Found", SafeMessage(ex), context),
            ArgumentException => Make(400, "Bad Request", SafeMessage(ex), context),

            ValidationException vex => new ProblemDetails
            {
                Status = 400,
                Title = "Validation Failed",
                Detail = "One or more validation errors occurred.",
                Instance = context?.Request.Path,
                Extensions = { ["errors"] = vex.Errors
                                    .GroupBy(e => e.PropertyName)
                                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()) }
            },

            DbUpdateException => Make(409, "Database Error", "A data conflict occurred.", context),

            _ => Make(500, "Internal Server Error", "An unexpected error occurred.", context)
        };

    public static ProblemDetails Build(DomainError error, HttpContext? context = null) =>
        new ProblemDetails
        {
            Title = error.Type.ToString(),
            Detail = error.Message,
            Status = MapStatusCode(error.Type),
            Instance = context?.Request.Path
        };
    
    public static int MapStatusCode(ErrorType type) =>
        type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

    private static ProblemDetails Make(int status, string title, string detail, HttpContext? context) =>
        new()
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = context?.Request.Path
        };

    private static string SafeMessage(Exception ex) =>
        ex is ArgumentException or ValidationException ? ex.Message : "An error occurred.";
}