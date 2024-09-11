using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using System.Collections.Immutable;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace SWO.Microservices.Dotnet.Shared.ApiExtensions;

public static class ActionResultExtensions
{
    private class ResultWithStatusCode<T> : ObjectResult
    {
        public ResultWithStatusCode(T content, HttpStatusCode httpStatusCode)
            : base(content)
        {
            base.StatusCode = (int)httpStatusCode;
        }
    }

    public static IActionResult ToActionResult<T>(this Result<T> result)
    {        
        return result.ToDto<T>().ToHttpStatusCode(result.HttpStatusCode);
    }

    public static async Task<IActionResult> ToActionResult<T>(this Task<Result<T>> result)
    {
        return (await result).ToActionResult();
    }

    private static IActionResult ToHttpStatusCode<T>(this T resultDto, HttpStatusCode statusCode)
    {
        return new ResultWithStatusCode<T>(resultDto, statusCode);
    }

    private static ResultDto<T> ToDto<T>(this Result<T> result)
    {
        if (result.Success)
        {
            return new ResultDto<T>
            {
                Value = result.Value,
                Errors = ImmutableArray<ErrorDto>.Empty
            };
        }

        return new ResultDto<T>
        {
            Value = default(T),
            Errors = result.Errors.Select((Error x) => new ErrorDto
            {
                ErrorCode = x.ErrorCode,
                Message = x.Message
            }).ToImmutableArray()
        };
    }
}
