using AutoMapper;
using MediatR;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Application.WeatherForecast.Dtos;

namespace SWO.Microservices.Dotnet.Shared.Application.WeatherForecast.Querys;


public record GetWeatherForecasts : IRequest<Result<PaginatedList<WeatherForecastDto>>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetWeatherForecastsHandler : IRequestHandler<GetWeatherForecasts, Result<PaginatedList<WeatherForecastDto>>>
{
    private readonly IMapper _mapper;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public GetWeatherForecastsHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<WeatherForecastDto>>> Handle(GetWeatherForecasts request, CancellationToken cancellationToken)
    {
        var weatherForecastsList = Enumerable.Range(1, 5).Select(index => new ApiDomain.Entities.WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();

        var mapWeatherForecastsList = _mapper.Map<List<WeatherForecastDto>>(weatherForecastsList.ToList());
        var result= PaginatedList<WeatherForecastDto>.Create(mapWeatherForecastsList.AsQueryable(), mapWeatherForecastsList.Count(), request.PageNumber, request.PageSize);

        return result;
    }
}