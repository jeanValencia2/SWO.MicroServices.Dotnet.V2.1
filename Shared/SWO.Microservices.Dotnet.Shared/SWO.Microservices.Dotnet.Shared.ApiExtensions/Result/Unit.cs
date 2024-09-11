namespace SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

public sealed class Unit
{
    public static readonly Unit Value = new Unit();
    private Unit() { }
}
