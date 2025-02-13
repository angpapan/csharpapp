using CSharpApp.Application.Exceptions;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharpApp.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (HttpRequestException ex)
        {
            await HandleExceptionAsync(httpContext, ex, ex.Message, (int)HttpStatusCode.BadRequest);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, ex, ex.Message, (int)HttpStatusCode.BadRequest, ex.Errors.ToList());
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, "Something went wrong.");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, string message,
        int statusCode = (int)HttpStatusCode.InternalServerError, List<string> errors = default)
    {
        _logger.LogError(exception, $"An exception occurred: {message}");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var errorResponse = new HttpError
        {
            StatusCode = statusCode,
            Message = message,
            Errors = errors
        };
        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        await context.Response.WriteAsync(json);
    }

    class HttpError
    {
        public int StatusCode { get; set; } = 500;
        public string Message { get; set; } = "";
        public List<string> Errors { get; set; } = new();
    }
}