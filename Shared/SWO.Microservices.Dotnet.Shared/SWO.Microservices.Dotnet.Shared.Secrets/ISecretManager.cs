
namespace SWO.Microservices.Dotnet.Shared.Secrets;

public interface ISecretManager
{
    Task<T> Get<T>(string path) where T : new();
}
