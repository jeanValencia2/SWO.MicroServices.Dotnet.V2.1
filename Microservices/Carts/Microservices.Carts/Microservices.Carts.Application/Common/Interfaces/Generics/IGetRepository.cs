namespace Microservices.Carts.Application.Common.Interfaces.Generics;

public interface IGetRepository<T> where T : class
{
    Task<T> GetAsync(Guid id);
}
