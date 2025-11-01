using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Api.Models.Common;

public static class ApiResponseExtensions
{
    
    public static ActionResult<ApiResponse<T>> ToApiResponse<T>(this T dto)
    {
        return new ObjectResult(ApiResponse<T>.Ok(dto));
    }
    
    public static ActionResult<ApiResponse> ToApiResponse(this DomainError error, HttpContext httpContext)
    {
        return new ObjectResult(ApiResponse.Fail(error, httpContext))
        {
            StatusCode = BuildProblemDetails.MapStatusCode(error.Type)
        };
    }

    public static ActionResult<ApiResponse<T>> ToApiResponse<T>(this DomainError error, HttpContext httpContext)
    {
        return new ObjectResult(ApiResponse<T>.Fail(error, httpContext))
        {
            StatusCode = BuildProblemDetails.MapStatusCode(error.Type)
        };
    }
}