using MediatR;
using Microservices.Customer.Application.Location.Commands;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Customer.Application.Location.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using System.Net;

namespace Microservices.Customer.Api.Controllers;

[ApiController]
[Authorize]
public class CityController : ApiController
{
    public CityController(IMediator? mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<CityDto>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateCity(CreateCity command)
    {
        return await Mediator.Send(command).ToActionResult();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResultDto<CityDto>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResultDto<ValidationProblemDetails>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateCity(Guid id, UpdateCity command)
    {
        if (id != command.Id)
        {
            return Result.BadRequest<BadRequestResult>(Error.Create("Bad Request", Guid.NewGuid())).ToActionResult();
        }
        return await Mediator.Send(command).ToActionResult();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResultDto<bool>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResultDto<ValidationProblemDetails>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteCity(Guid id)
    {
        return await Mediator.Send(new DeleteCity { Id = id }).ToActionResult();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultDto<PaginatedList<CityDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCities([FromQuery] GetCities query)
    {
        return await Mediator.Send(query).ToActionResult();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResultDto<CityDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCityById(Guid id)
    {
        return await Mediator.Send(new GetCityById { Id = id }).ToActionResult();
    }
}
