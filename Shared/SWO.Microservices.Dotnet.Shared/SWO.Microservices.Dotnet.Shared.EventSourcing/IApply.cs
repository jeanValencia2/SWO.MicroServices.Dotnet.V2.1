namespace SWO.Microservices.Dotnet.Shared.EventSourcing;

public interface IApply<T>
{
    void Apply(T ev);
}