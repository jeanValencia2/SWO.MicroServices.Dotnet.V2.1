
namespace SWO.Microservices.Dotnet.Shared.Discovery;

public record DiscoveryData(string Server, int Port);

public interface IServiceDiscovery
{
    Task<string> GetFullAddress(string serviceKey, CancellationToken cancellationToken = default);
    Task<DiscoveryData> GetDiscoveryData(string serviceKey, CancellationToken cancellationToken = default);
}
