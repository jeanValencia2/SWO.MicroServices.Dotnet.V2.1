using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Exceptions;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;

namespace SWO.Microservices.Dotnet.Shared.ApiExtensions.Filters;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;
    private readonly ILogger<CustomExceptionHandler> _logger;
    public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    {
        _logger = logger;

        // Register known exception types and handlers.
        _exceptionHandlers = new()
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
                { typeof(InvalidOperationException), HandleInvalidOperationException },
            };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        _logger.LogError(exception, exception.Message);

        if (_exceptionHandlers.ContainsKey(exceptionType))
        {
            await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
            return true;
        }

        return false;
    }

    private async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = (ValidationException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;        

        ResultDto<ValidationProblemDetails> result = new ResultDto<ValidationProblemDetails>();
        result.Value = new ValidationProblemDetails(exception.Errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Detail = exception.Message
        };
        result.Errors = [new ErrorDto { Message = exception.Message, ErrorCode = Guid.NewGuid() }];

        await httpContext.Response.WriteAsJsonAsync(result);
    }

    private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        var exception = (NotFoundException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        ResultDto<ProblemDetails> result = new ResultDto<ProblemDetails>();
        result.Value = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };
        result.Errors = [new ErrorDto { Message = exception.Message, ErrorCode = Guid.NewGuid() }];

        await httpContext.Response.WriteAsJsonAsync(result);
    }

    private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;        

        ResultDto<ProblemDetails> result = new ResultDto<ProblemDetails>();
        result.Value = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Title = "Unauthorized",
        };
        result.Errors = [new ErrorDto { Message = "Unauthorized", ErrorCode = Guid.NewGuid() }];

        await httpContext.Response.WriteAsJsonAsync(result);
    }

    private async Task HandleForbiddenAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;        

        ResultDto<ProblemDetails> result = new ResultDto<ProblemDetails>();
        result.Value = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };
        result.Errors = [new ErrorDto { Message = "Forbidden", ErrorCode = Guid.NewGuid() }];

        await httpContext.Response.WriteAsJsonAsync(result);
    }

    private async Task HandleInvalidOperationException(HttpContext httpContext, Exception ex)
    {
        ResultDto<ProblemDetails> result = new ResultDto<ProblemDetails>();
        result.Value = new ProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Title = "Invalid Operation",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
        result.Errors = [new ErrorDto { Message = ex.Message, ErrorCode = Guid.NewGuid() }];

        await httpContext.Response.WriteAsJsonAsync(result);
    }
}
