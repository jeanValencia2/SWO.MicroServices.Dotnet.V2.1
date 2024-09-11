using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Behaviours;
using System.Reflection;

namespace Microservices.Customer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });


        return services;
    }
}
