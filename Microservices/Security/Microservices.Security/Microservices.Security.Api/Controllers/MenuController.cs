using MediatR;
using Microservices.Security.Application.Identity.Dtos;
using Microservices.Security.Application.Menu.Commands;
using Microservices.Security.Application.Menu.Querys;
using Microservices.Security.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using System.Net;

namespace Microservices.Security.Api.Controllers;

[ApiController]
[Authorize]
public class MenuController : ApiController
{
    public MenuController(IMediator? mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<ApplicationClientDto>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateMenu(CreateMenu menu)
    {
        return await Mediator.Send(menu).ToActionResult();
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResultDto<PaginatedList<MenuDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetMenus([FromQuery] GetMenus menu)
    {
        return await Mediator.Send(menu).ToActionResult();
    }

    [HttpGet("byuser")]
    [ProducesResponseType(typeof(ResultDto<PaginatedList<MenuDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetMenusByUser([FromQuery] GetMenusByUser menu)
    {
        return await Mediator.Send(menu).ToActionResult();
    }
}
