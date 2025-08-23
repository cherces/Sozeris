using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sozeris.Server.Api.Models.Common;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    
    [JsonConverter(typeof(ExceptionJsonConverter))] 
    public Exception? Exception { get; }

    protected ApiResponse(bool success, string message, Exception? exception = null)
    {
        Success = success;
        Message = message;
        Exception = exception;
    }
    
    public static ApiResponse Ok() => new ApiResponse(true, string.Empty);
    public static ApiResponse Fail(Exception ex) => new ApiResponse(false, ex.Message, ex);
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    private ApiResponse(bool success, string message, T? data, Exception? exception = null) 
        : base(success, message, exception)
    {
        Data = data;
    }
    
    public static ApiResponse<T> Ok(T data) => 
        new ApiResponse<T>(true, String.Empty, data);
    public new static ApiResponse<T> Fail(Exception ex) => 
        new ApiResponse<T>(false, ex.Message, default, ex);
    
    public static ApiResponse<T> Fail(ModelStateDictionary modelState)
    {
        var errors = modelState
            .Where(ms => ms.Value.Errors.Count > 0)
            .ToDictionary(
                k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var ex = new ValidationException("Ошибка валидации", errors);
        return Fail(ex);
    }
}
