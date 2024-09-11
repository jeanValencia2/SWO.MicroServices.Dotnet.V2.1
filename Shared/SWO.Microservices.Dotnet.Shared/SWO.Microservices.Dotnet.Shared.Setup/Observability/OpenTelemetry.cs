using SWO.Microservices.Dotnet.Shared.Discovery;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace SWO.Microservices.Dotnet.Shared.Setup.Observability;

public static class OpenTelemetry
{
    private static string? _openTelemetryUrl;

#pragma warning disable CS8604 // Possible null reference argument.

    public static void AddTracing(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        serviceCollection.AddOpenTelemetry().WithTracing(builder => builder
           .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(configuration["AppName"]))
           .AddAspNetCoreInstrumentation()
           .AddOtlpExporter(exporter =>
           {
               string url = GetOpenTelemetryCollectorUrl(serviceCollection.BuildServiceProvider()).Result;
               exporter.Endpoint = new Uri(url);
           })
       );


    }

    public static void AddMetrics(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddOpenTelemetry().WithMetrics(builder => builder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(configuration["AppName"]))
            .AddAspNetCoreInstrumentation()
            .AddOtlpExporter(exporter =>
            {
                string url = GetOpenTelemetryCollectorUrl(serviceCollection.BuildServiceProvider()).Result;
                exporter.Endpoint = new Uri(url);
            })
        );       
    }

    public static void AddLogging(this IHostBuilder builder, IConfiguration configuration)
    {
        builder.ConfigureLogging(logging => logging                
                .ClearProviders()
                .AddOpenTelemetry(options =>
                {
                    options.IncludeFormattedMessage = true;
                    options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(configuration["AppName"]));
                    options.AddConsoleExporter();
                }));
    }

#pragma warning restore CS8604 // Possible null reference argument.

    private static async Task<string> GetOpenTelemetryCollectorUrl(IServiceProvider serviceProvider)
    {
        if (_openTelemetryUrl != null)
            return _openTelemetryUrl;


        var serviceDiscovery = serviceProvider.GetService<IServiceDiscovery>();
        string openTelemetryLocation = await serviceDiscovery?.GetFullAddress(DiscoveryServices.OpenTelemetry)!;
        _openTelemetryUrl = $"http://{openTelemetryLocation}";
        return _openTelemetryUrl;
    }

}
