using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SWO.Microservices.Dotnet.Shared.Setup.API;

[Route("api/v1/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private IMediator? _mediator;

    public ApiController(IMediator? mediator)
    {
        _mediator = mediator;
    }

    protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>() ?? throw new InvalidOperationException("IMediator is not registered.");

}
