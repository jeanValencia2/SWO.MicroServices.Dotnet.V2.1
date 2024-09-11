using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Serilog;
using NSwag;
using NSwag.Generation.Processors.Security;
using SWO.Microservices.Dotnet.Shared.Serialization;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using SWO.Microservices.Dotnet.Shared.Setup.Observability;
using SWO.Microservices.Dotnet.Shared.Discovery;
using SWO.Microservices.Dotnet.Shared.Loggins.Graylog;

namespace SWO.Microservices.Dotnet.Shared.Setup.API
{
    public static class DefaultWebApplication
    {
        public static WebApplication Create(string[] args, Action<WebApplicationBuilder>? webappBuilder = null)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddConfiguration(HealthCheckHelper.BuildBasicHealthCheck());
            builder.Services.AddHealthChecks();
            builder.Services.AddHealthChecksUI().AddInMemoryStorage();
            builder.Services.AddControllers();            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApiDocument((configure, sp) =>
            {
                configure.Title = "SWO Microservices .Net";

                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
            builder.Services.AddRouting(x => x.LowercaseUrls = true);
            builder.Services.AddSerializer();

            builder.Services.AddServiceDiscovery(builder.Configuration);
            builder.Services.AddSecretManager(builder.Configuration);            
            builder.Services.AddLogging(logger => logger.AddSerilog());
            builder.Services.AddTracing(builder.Configuration);
            builder.Services.AddMetrics(builder.Configuration);

            builder.Host.ConfigureSerilog(builder.Services.BuildServiceProvider().GetRequiredService<IServiceDiscovery>());

            if (webappBuilder != null)
            {
                webappBuilder.Invoke(builder);
            }

            return builder.Build();
        }

        public static void Run(WebApplication webApp, Assembly assembly)
        {
            webApp.UseHttpsRedirection();
            webApp.UseStaticFiles();

            webApp.UseSwaggerUi(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            webApp.MapHealthChecks("/health");
            webApp.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            webApp.UseHealthChecksUI(config =>
            {
                config.UIPath = "/health-ui";
            });

            webApp.UseExceptionHandler(options => { });

            webApp.Map("/", () => Results.Redirect("/api"));

            webApp.UseRouting();
            webApp.UseAuthentication();
            webApp.UseAuthorization();
            webApp.MapControllers();            
            webApp.Run();
        }
    }
}
