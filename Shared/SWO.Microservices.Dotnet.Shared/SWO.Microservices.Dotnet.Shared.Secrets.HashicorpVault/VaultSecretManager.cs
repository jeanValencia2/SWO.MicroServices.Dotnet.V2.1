using Microsoft.Extensions.Options;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;
using VaultSharp.V1.SecretsEngines;

namespace SWO.Microservices.Dotnet.Shared.Secrets.HashicorpVault;


internal class VaultSecretManager : ISecretManager
{
    private readonly VaultSettings _vaultSettings;

    public VaultSecretManager(IOptions<VaultSettings> vaultSettings)
    {
        _vaultSettings = vaultSettings.Value with { TokenApi = GetTokenFromEnvironmentVariable() };
    }

    public async Task<T> Get<T>(string path) where T : new()
    {
        VaultClient client = new VaultClient(new VaultClientSettings(_vaultSettings.VaultUrl,new TokenAuthMethodInfo(_vaultSettings.TokenApi)));

        Secret<SecretData<T>> kv2Secret = await client.V1.Secrets.KeyValue.V2.ReadSecretAsync<T>(path: path, mountPoint: "secret");
        var returnedData = kv2Secret.Data.Data;

        return returnedData;
    }

    public async Task<UsernamePasswordCredentials> GetRabbitMQCredentials(string roleName)
    {
        VaultClient client = new VaultClient(new VaultClientSettings(_vaultSettings.VaultUrl,
            new TokenAuthMethodInfo(_vaultSettings.TokenApi)));

        Secret<UsernamePasswordCredentials> secret = await client.V1.Secrets.RabbitMQ
            .GetCredentialsAsync(roleName, "rabbitmq");
        return secret.Data;
    }

    private string GetTokenFromEnvironmentVariable()
        => Environment.GetEnvironmentVariable("VAULT_TOKEN") 
           ?? throw new NotImplementedException("please specify the VAULT_TOKEN env_var");
}