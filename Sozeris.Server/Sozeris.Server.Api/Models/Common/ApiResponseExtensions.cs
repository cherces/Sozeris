using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Api.Models.Common;

public static class ApiResponseExtensions
{
    public static ActionResult<ApiResponse> ToApiResponse(this ControllerBase controller)
    {
        return controller.Ok(ApiResponse.Ok());
    }

    public static ActionResult<ApiResponse> ToApiResponse(
        this DomainError error, ControllerBase controller)
    {
        return controller.StatusCode(
            BuildProblemDetails.MapStatusCode(error.Type),
            ApiResponse.Fail(error, controller.HttpContext));
    }

    public static ActionResult<ApiResponse<T>> ToApiResponse<T>(
        this T dto, ControllerBase controller)
    {
        return controller.Ok(ApiResponse<T>.Ok(dto));
    }

    public static ActionResult<ApiResponse<T>> ToApiResponse<T>(
        this DomainError error, ControllerBase controller)
    {
        return controller.StatusCode(
            BuildProblemDetails.MapStatusCode(error.Type),
            ApiResponse<T>.Fail(error, controller.HttpContext));
    }

    public static ActionResult<ApiResponse<T>> ToCreatedAt<T>(
        this T dto, ControllerBase controller, string actionName, object routeValues)
    {
        return controller.CreatedAtAction(actionName, routeValues, ApiResponse<T>.Ok(dto));
    }
}