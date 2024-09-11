using Microservices.Security.Application;
using Microservices.Security.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Filters;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using System.Reflection;
using System.Text;

WebApplication app = DefaultWebApplication.Create(args, webappBuilder =>
{
    webappBuilder.Services.AddTransient<IServiceToken, ServiceToken>();
    webappBuilder.Services.AddApplication();
    webappBuilder.Services.AddInfrastructure(webappBuilder.Configuration);
    webappBuilder.Services.AddExceptionHandler<CustomExceptionHandler>();
});


var assembly = Assembly.GetExecutingAssembly();

DefaultWebApplication.Run(app, assembly);