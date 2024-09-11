using Microsoft.Extensions.DependencyInjection;

namespace SWO.Microservices.Dotnet.Shared.Serialization
{
    public static class SerializationDependencyInjection
    {
        public static void AddSerializer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISerializer, Serializer>();
        }
    }
}
