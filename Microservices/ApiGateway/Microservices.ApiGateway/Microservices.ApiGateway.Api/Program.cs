
using SWO.Microservices.Dotnet.Shared.Setup.API;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using System.Reflection;

WebApplication app = DefaultWebApplication.Create(args, webappBuilder =>
{
    webappBuilder.Services.AddReverseProxy()
        .LoadFromConfig(webappBuilder.Configuration.GetSection("ReverseProxy"));
    webappBuilder.Services.AddServiceBusIntegrationPublisher(webappBuilder.Configuration);
});

app.MapGet("/", () => "Hello World!");

app.MapReverseProxy();

var assembly = Assembly.GetExecutingAssembly();

DefaultWebApplication.Run(app, assembly);