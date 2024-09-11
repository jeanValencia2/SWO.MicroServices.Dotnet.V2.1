using MediatR;
using Microservices.Security.Application.Identity.Commands;
using Microservices.Security.Application.Identity.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using System.Net;

namespace Microservices.Security.Api.Controllers;

[ApiController]
[Authorize]
public class ApplicationClientController : ApiController
{
    public ApplicationClientController(IMediator? mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<ApplicationClientDto>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateApplicationClient(CreateApplicationClient client)
    {
        return await Mediator.Send(client).ToActionResult();
    }
}
