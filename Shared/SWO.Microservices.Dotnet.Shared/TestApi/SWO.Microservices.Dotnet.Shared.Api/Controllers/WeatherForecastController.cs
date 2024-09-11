using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.Application.WeatherForecast.Dtos;
using SWO.Microservices.Dotnet.Shared.Application.WeatherForecast.Querys;
using SWO.Microservices.Dotnet.Shared.ApiExtensions;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using System.Net;

namespace SWO.Microservices.Dotnet.Shared.Api.Controllers
{    
    public class WeatherForecastController : ApiController
    {
        public WeatherForecastController(IMediator? mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultDto<PaginatedList<WeatherForecastDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            return await Mediator.Send(new GetWeatherForecasts()).ToActionResult();
        }
    }
}
