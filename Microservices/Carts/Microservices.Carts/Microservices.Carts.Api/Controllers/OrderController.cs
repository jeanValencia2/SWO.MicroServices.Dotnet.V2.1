using MediatR;
using Microservices.Carts.Application.Dtos;
using Microservices.Carts.Application.Orders.Commands;
using Microservices.Carts.Application.Orders.Querys;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using System.Net;

namespace Microservices.Carts.Api.Controllers;

public class OrderController : ApiController
{
    public OrderController(IMediator? mediator) : base(mediator)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultDto<PaginatedList<OrderDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get()
    {
        return await Mediator.Send(new GetOrders()).ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<OrderDto>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrder command)
    {
        return await Mediator.Send(command).ToActionResult();
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResultDto<OrderDto>), (int)HttpStatusCode.Accepted)]
    public async Task<IActionResult> UpdateStatusOrder([FromBody] UpdateStatusOrder command)
    {
        return await Mediator.Send(command).ToActionResult();
    }

}
