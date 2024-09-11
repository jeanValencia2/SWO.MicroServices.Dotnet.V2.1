using Microservices.Products.Application;
using Microservices.Products.Infrastructure;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Filters;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using System.Reflection;

WebApplication app = DefaultWebApplication.Create(args, builder =>
{
    builder.Services.AddServiceBusDomainPublisher(builder.Configuration);
    builder.Services.AddServiceBusIntegrationPublisher(builder.Configuration);
    builder.Services.AddTransient<IServiceToken, ServiceToken>();
    builder.Services.AddScoped<ICurrentUser, CurrentUser>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();    
});

var assembly = Assembly.GetExecutingAssembly();

DefaultWebApplication.Run(app, assembly);