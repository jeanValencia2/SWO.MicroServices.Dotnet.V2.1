namespace Microservices.Carts.Domain.ValueObjects;

public class PaymentInformation
{
    public string? CardNumber { get; set; }
    public string? ExpireDate { get; set; }
    public string? Security { get; set; }
}