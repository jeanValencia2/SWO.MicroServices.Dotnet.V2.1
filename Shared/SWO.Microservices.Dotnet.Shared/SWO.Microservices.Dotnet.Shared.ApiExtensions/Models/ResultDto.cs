﻿using System.Collections.Immutable;

namespace SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;

public class ResultDto<T>
{
    public T? Value { get; set; }

    public ImmutableArray<ErrorDto> Errors { get; set; }

    public bool Success => Errors.Length == 0;
}
