using Microservices.Carts.Application;
using Microservices.Carts.Infrastructure;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Filters;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using System.Reflection;

WebApplication app = DefaultWebApplication.Create(args, webappBuilder =>
{
    webappBuilder.Services.AddEventSourcing(webappBuilder.Configuration);
    webappBuilder.Services.AddApplication();
    webappBuilder.Services.AddInfrastructure(webappBuilder.Configuration);
    webappBuilder.Services.AddExceptionHandler<CustomExceptionHandler>();
});

var assembly = Assembly.GetExecutingAssembly();

DefaultWebApplication.Run(app, assembly);
