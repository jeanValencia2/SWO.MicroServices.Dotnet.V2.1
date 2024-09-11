using SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Host;

public class ConsumerController<TMessage> : Controller
{
    private readonly IConsumerManager<TMessage> _consumerManager;

    public ConsumerController(IConsumerManager<TMessage> consumerManager)
    {
        _consumerManager = consumerManager;
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("start")]
    public virtual IActionResult Start()
    {
        _consumerManager.RestartExecution();
        return Ok();
    }
}