namespace SWO.Microservices.Dotnet.Shared.Discovery;

public class DiscoveryServices
{
    public const string RabbitMQ = "RabbitMQ";
    public const string Secrets = "SecretManager";
    public const string SQLServer = "SQLServer";
    public const string MySql = "MySql";
    public const string MongoDb = "MongoDb";
    public const string Graylog = "Graylog";
    public const string AppInsights = "AppInsights";
    public const string OpenTelemetry = "OpenTelemetryCollector";

    public class Microservices
    {
        public const string Security = "SecurityApi";
        public const string Customer = "CustomerApi";
        public const string Products = "ProductsApi";
        public const string Carts = "CartsApi";
    }
}
