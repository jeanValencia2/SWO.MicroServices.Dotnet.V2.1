using System.Reflection;
using SWO.Microservices.Dotnet.Shared.Comunication.Messages;

namespace SWO.Microservices.Dotnet.Shared.Comunication.Publisher.Domain;

public class DomainMessageMapper
{
    public static DomainMessage MapToMessage(object message, Metadata metadata)
    {
        if (message is IntegrationMessage)
            throw new ArgumentException("Message should not be of type DomainMessage, it should be a plain type");

        var buildWrapperMethodInfo = typeof(DomainMessageMapper).GetMethod(
            nameof(ToTypedIntegrationEvent),
            BindingFlags.Static | BindingFlags.NonPublic
        );
        
        var buildWrapperGenericMethodInfo = buildWrapperMethodInfo?.MakeGenericMethod(new[] {message.GetType()});
        var wrapper = buildWrapperGenericMethodInfo?.Invoke(
            null,
            new[]
            {
                message,
                metadata
            }
        );
        return (wrapper as DomainMessage)!;
    }
    
    
    private static DomainMessage<T> ToTypedIntegrationEvent<T>(T message, Metadata metadata)
    {
        return new DomainMessage<T>(Guid.NewGuid().ToString(), typeof(T).Name, message, metadata);
    }
}