using SWO.Microservices.Dotnet.Shared.Application;
using SWO.Microservices.Dotnet.Shared.Infrastructure;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Filters;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using System.Reflection;

WebApplication app = DefaultWebApplication.Create(args, webappBuilder =>
{
    webappBuilder.Services.AddApplication();
    webappBuilder.Services.AddInfrastructure(webappBuilder.Configuration);
    webappBuilder.Services.AddExceptionHandler<CustomExceptionHandler>();
    webappBuilder.Services.AddServiceBusIntegrationPublisher(webappBuilder.Configuration);
});


var assembly = Assembly.GetExecutingAssembly();

DefaultWebApplication.Run(app, assembly);