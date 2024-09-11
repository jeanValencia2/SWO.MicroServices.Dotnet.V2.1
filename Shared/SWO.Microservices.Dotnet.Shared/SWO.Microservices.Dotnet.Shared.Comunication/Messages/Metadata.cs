namespace SWO.Microservices.Dotnet.Shared.Comunication.Messages;

public record Metadata
{
    public string CorrelationId { get; }
    public DateTime CreatedUtc { get; }

    public Metadata(string correlationId, DateTime createdUtc)
    {
        CorrelationId = correlationId;
        CreatedUtc = createdUtc;
    }
}