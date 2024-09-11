namespace SWO.Microservices.Dotnet.Shared.Comunication.Consumer.Manager;

public interface IConsumerManager<TMessage>
{
    void RestartExecution();
    void StopExecution();
    CancellationToken GetCancellationToken();
}