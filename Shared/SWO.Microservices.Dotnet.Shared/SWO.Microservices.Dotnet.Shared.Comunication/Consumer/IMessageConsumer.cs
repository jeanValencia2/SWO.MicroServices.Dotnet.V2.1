namespace SWO.Microservices.Dotnet.Shared.Comunication.Consumer;

public interface IMessageConsumer
{
    Task StartAsync(CancellationToken cancelToken = default);
}

public interface IMessageConsumer<T> : IMessageConsumer
{
}