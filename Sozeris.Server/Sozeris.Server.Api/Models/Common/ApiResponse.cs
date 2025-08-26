using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Api.Models.Common;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    
    public ProblemDetails? Error { get; set; }

    protected ApiResponse(bool isSuccess, ProblemDetails? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static ApiResponse Ok() => new ApiResponse(true);
    public static ApiResponse Fail(Exception ex, HttpContext? context) => new ApiResponse(false, BuildProblemDetails.Build(ex, context));
    public static ApiResponse Fail(DomainError error, HttpContext? context) => new ApiResponse(false, BuildProblemDetails.Build(error, context));
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    private ApiResponse(bool isSuccess, T? data = default, ProblemDetails? error = null) 
        : base(isSuccess, error)
    {
        Data = data;
    }
    
    public static ApiResponse<T> Ok(T data) => new ApiResponse<T>(true, data);
    public new static ApiResponse<T> Fail(Exception ex, HttpContext? context = null) =>
        new ApiResponse<T>(false, default, BuildProblemDetails.Build(ex, context));
    public new static ApiResponse<T> Fail(DomainError error, HttpContext? context = null) =>
        new ApiResponse<T>(false, default, BuildProblemDetails.Build(error, context));
}
