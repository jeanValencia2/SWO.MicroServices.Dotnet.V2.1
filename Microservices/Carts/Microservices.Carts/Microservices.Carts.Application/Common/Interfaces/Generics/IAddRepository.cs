namespace Microservices.Carts.Application.Common.Interfaces.Generics;
public interface IAddRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
}
