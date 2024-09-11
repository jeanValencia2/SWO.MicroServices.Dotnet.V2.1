namespace SWO.Microservices.Dotnet.Shared.Domain.Entities;

public record TokenSecrets
{
    public string Key { get; set; } = null!;
    public string Lifetime { get; set; } = null!;
    public string RefreshTokenExpiryTime { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
}

public record TokenResult
{
    public string? Type { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime ExpireIn { get; set; }
}
