using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.Models.Common;

namespace Sozeris.Server.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = ApiResponse.Fail(ex, context);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.Error?.Status ?? StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));        
        }
    }
}