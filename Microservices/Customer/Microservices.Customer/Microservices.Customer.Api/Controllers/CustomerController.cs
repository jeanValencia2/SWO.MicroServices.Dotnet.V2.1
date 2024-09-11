using MediatR;
using Microservices.Customer.Application.Customer.Commands;
using Microservices.Customer.Application.Customer.Dtos;
using Microservices.Customer.Application.Customer.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using System.Net;

namespace Microservices.Customer.Api.Controllers;

[ApiController]
public class CustomerController : ApiController
{
    public CustomerController(IMediator? mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<CustomerDto>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateCustomer(CreateCustomer command)
    {
        return await Mediator.Send(command).ToActionResult();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResultDto<CustomerDto>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResultDto<ValidationProblemDetails>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomer command)
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
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        return await Mediator.Send(new DeleteCustomer { Id = id }).ToActionResult();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResultDto<PaginatedList<CustomerDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCustomers([FromQuery] GetCustomers query)
    {
        return await Mediator.Send(query).ToActionResult();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResultDto<CustomerDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        return await Mediator.Send(new GetCustomerById { Id = id }).ToActionResult();
    }

    [HttpPost("shippingaddress")]
    [ProducesResponseType(typeof(ResultDto<CustomerDto>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateShippingAddress(CreateShippingAddress command)
    {
        return await Mediator.Send(command).ToActionResult();
    }

    [HttpPut("shippingaddress")]
    [ProducesResponseType(typeof(ResultDto<CustomerDto>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResultDto<ValidationProblemDetails>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateShippingAddress(UpdateShippingAddress command)
    {        
        return await Mediator.Send(command).ToActionResult();
    }

    [HttpDelete("shippingaddress")]
    [ProducesResponseType(typeof(ResultDto<bool>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResultDto<ValidationProblemDetails>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteShippingAddress(DeleteShippingAddress command)
    {
        return await Mediator.Send(command).ToActionResult();
    }
}
