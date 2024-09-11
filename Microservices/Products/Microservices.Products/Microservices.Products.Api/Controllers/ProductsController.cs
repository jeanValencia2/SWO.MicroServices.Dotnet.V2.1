using MediatR;
using Microservices.Products.Application.Products.Commands;
using Microservices.Products.Application.Products.Dtos;
using Microservices.Products.Application.Products.Querys;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using System.Net;

namespace Microservices.Products.Api.Controllers
{
    [ApiController]
    public class ProductsController : ApiController
    {
        public ProductsController(IMediator? mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultDto<ProductDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct(CreateProduct command)
        {
            return await Mediator.Send(command).ToActionResult();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultDto<ProductDto>), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResultDto<ValidationProblemDetails>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProduct command)
        {
            if (id != command.Id)
            {
                return Result.BadRequest<BadRequestResult>(Error.Create("Bad Request", Guid.NewGuid())).ToActionResult();
            }
            return await Mediator.Send(command).ToActionResult();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultDto<PaginatedList<ProductDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProducts([FromQuery] GetProducts query)
        {
            return await Mediator.Send(query).ToActionResult();
        }
    }
}
